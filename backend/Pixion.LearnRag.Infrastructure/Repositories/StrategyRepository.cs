using System.Data;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Npgsql;
using NpgsqlTypes;
using Pgvector;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Interfaces;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public abstract class
    StrategyRepository<TMetadata, TEmbeddingOptions> : IStrategyRepository<TMetadata, TEmbeddingOptions>
    where TMetadata : IMetadata
    where TEmbeddingOptions : struct, IEmbeddingOptions
{
    protected readonly NpgsqlDataSource Npgsql;
    protected readonly string Schema;
    protected readonly string Table;

    protected StrategyRepository(IOptions<VectorDatabaseConfig> config, string table)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(config.Value.DatabaseConnection);
        dataSourceBuilder.UseVector();
        Npgsql = dataSourceBuilder.Build();
        Table = table;
        Schema = config.Value.Schema;
    }

    public async Task InsertAsync(
        EmbeddingRecord<TMetadata> embeddingRecord,
        CancellationToken cancellationToken = default
    )
    {
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               INSERT
                               INTO {Schema}.{Table} (id, text, embedding, metadata)
                               VALUES (@id, @text, @embedding, @metadata)
                               """;

            cmd.Parameters.AddWithValue(
                "@id",
                embeddingRecord.Id
            );
            cmd.Parameters.AddWithValue(
                "@text",
                embeddingRecord.Text
            );
            cmd.Parameters.AddWithValue(
                "@embedding",
                embeddingRecord.Embedding
            );
            cmd.Parameters.AddWithValue(
                "@metadata",
                NpgsqlDbType.Jsonb,
                embeddingRecord.Metadata
            );

            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    public async Task BatchInsertAsync(
        IEnumerable<EmbeddingRecord<TMetadata>> embeddingRecords,
        CancellationToken cancellationToken = default
    )
    {
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            var embeddingRecordsList = embeddingRecords.ToList();

            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               INSERT
                               INTO {Schema}.{Table} (id, TEXT, embedding, metadata)
                               SELECT
                                   bulk.*
                               FROM
                                   (SELECT *
                                    FROM
                                        UNNEST(@id, @text, @embedding, @metadata::jsonb[]) AS t(id, text, embedding, metadata)
                                   ) AS bulk
                               """;

            cmd.Parameters.AddWithValue(
                "@id",
                embeddingRecordsList
                    .Select(x => x.Id)
                    .ToArray()
            );
            cmd.Parameters.AddWithValue(
                "@text",
                embeddingRecordsList
                    .Select(x => x.Text)
                    .ToArray()
            );
            cmd.Parameters.AddWithValue(
                "@embedding",
                embeddingRecordsList
                    .Select(x => new Vector(x.Embedding))
                    .ToArray()
            );
            cmd.Parameters.AddWithValue(
                "@metadata",
                NpgsqlDbType.Array | NpgsqlDbType.Jsonb,
                embeddingRecordsList
                    .Select(x => JsonSerializer.Serialize(x.Metadata))
                    .ToArray()
            );

            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<TEmbeddingOptions>> GetDocumentEmbeddingOptionsAsync(
        Guid documentId,
        CancellationToken cancellationToken = default
    )
    {
        var results = new List<TEmbeddingOptions>();
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               SELECT DISTINCT
                                   metadata->'EmbeddingOptions' as embedding_options
                               FROM
                                   {Schema}.{Table}
                               WHERE
                                   metadata @> jsonb_build_object('DocumentId', @documentId)
                               """;

            cmd.Parameters.AddWithValue("@documentId", documentId);

            await using var dataReader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await dataReader.ReadAsync(cancellationToken))
            {
                var json = dataReader.GetFieldValue<string>(dataReader.GetOrdinal("embedding_options"));
                var options = JsonSerializer.Deserialize<TEmbeddingOptions>(json);
                results.Add(options);
            }

            return results;
        }
    }

    public async Task<IEnumerable<EmbeddingRecord<TMetadata>>> PreviewAsync(
        Guid documentId,
        TEmbeddingOptions embeddingOptions,
        int limit = 10,
        CancellationToken cancellationToken = default
    )
    {
        var previewResults = new List<EmbeddingRecord<TMetadata>>();
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               SELECT *
                               FROM {Schema}.{Table}
                               WHERE
                                   metadata @> jsonb_build_object(
                                       'EmbeddingOptions', @embeddingOptions,
                                       'DocumentId', @documentId
                                  )
                               LIMIT @limit
                               """;

            cmd.Parameters.AddWithValue("@documentId", documentId);
            cmd.Parameters.AddWithValue(
                "@embeddingOptions",
                NpgsqlDbType.Jsonb,
                JsonSerializer.Serialize(embeddingOptions)
            );
            cmd.Parameters.AddWithValue("@limit", limit);

            await using var dataReader = await cmd.ExecuteReaderAsync(
                CommandBehavior.SequentialAccess,
                cancellationToken
            );

            while (await dataReader.ReadAsync(cancellationToken))
                previewResults.Add(await ReadEmbeddingRecordAsync(dataReader, cancellationToken));

            return previewResults;
        }
    }

    public async Task<IEnumerable<SearchResult<TMetadata>>> DefaultSearchAsync(
        ReadOnlyMemory<float> queryEmbedding,
        TEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    )
    {
        var searchResults = new List<SearchResult<TMetadata>>();
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               SELECT *
                               FROM (
                                    SELECT id, text, metadata,
                                           1 - (embedding <=> @embedding) AS cosine_similarity
                                    FROM {Schema}.{Table}
                                    WHERE
                                        metadata @> jsonb_build_object(
                                            'EmbeddingOptions', @embeddingOptions
                                        )
                                    ) AS relevance_table
                               ORDER BY cosine_similarity DESC
                               LIMIT @limit;
                               """;

            cmd.Parameters.AddWithValue(
                "@embeddingOptions",
                NpgsqlDbType.Jsonb,
                JsonSerializer.Serialize(embeddingOptions)
            );
            cmd.Parameters.AddWithValue("@embedding", new Vector(queryEmbedding));
            cmd.Parameters.AddWithValue("@limit", limit);

            await using var dataReader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await dataReader.ReadAsync(cancellationToken)) searchResults.Add(ReadSearchResult(dataReader));

            return searchResults;
        }
    }

    protected SearchResult<TMetadata> ReadSearchResult(
        NpgsqlDataReader dataReader
    )
    {
        var id = dataReader.GetGuid(dataReader.GetOrdinal("id"));
        var text = dataReader.GetString(dataReader.GetOrdinal("text"));
        var metadataString = dataReader.GetString(dataReader.GetOrdinal("metadata"));

        double? relevance = null;
        try
        {
            relevance = dataReader.GetDouble(dataReader.GetOrdinal("cosine_similarity"));
        }
        catch (IndexOutOfRangeException)
        {
        }

        return new SearchResult<TMetadata>(id, text, relevance, metadataString);
    }

    protected async Task<EmbeddingRecord<TMetadata>> ReadEmbeddingRecordAsync(
        NpgsqlDataReader dataReader,
        CancellationToken cancellationToken = default
    )
    {
        var id = dataReader.GetGuid(dataReader.GetOrdinal("id"));
        var text = dataReader.GetString(dataReader.GetOrdinal("text"));
        var embedding = await dataReader
            .GetFieldValueAsync<Vector>(dataReader.GetOrdinal("embedding"), cancellationToken)
            .ConfigureAwait(false);
        var metadataString = dataReader.GetString(dataReader.GetOrdinal("metadata"));

        return new EmbeddingRecord<TMetadata>(id, text, embedding.Memory, metadataString);
    }
}