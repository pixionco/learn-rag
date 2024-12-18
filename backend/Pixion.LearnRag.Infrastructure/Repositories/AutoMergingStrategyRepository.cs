using System.Text.Json;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Pgvector;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class AutoMergingStrategyRepository(IOptions<VectorDatabaseConfig> config)
    : StrategyRepository<AutoMergingMetadata, AutoMergingEmbeddingOptions>(config, Strategy.AutoMerging),
      IAutoMergingStrategyRepository
{
    public async Task<SearchResult<AutoMergingMetadata>?> GetParentChunkAsync(
        Guid documentId,
        int parentIndex,
        AutoMergingEmbeddingOptions embeddingOptions,
        CancellationToken cancellationToken
    )
    {
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
                                        'Index', @parentIndex,
                                        'DocumentId', @documentId
                                    )
                               """;
            cmd.Parameters.AddWithValue("@documentId", documentId);
            cmd.Parameters.AddWithValue("@parentIndex", parentIndex);
            cmd.Parameters.AddWithValue(
                "@embeddingOptions",
                NpgsqlDbType.Jsonb,
                JsonSerializer.Serialize(embeddingOptions)
            );

            await using var dataReader = await cmd.ExecuteReaderAsync(cancellationToken);
            if (await dataReader.ReadAsync(cancellationToken)) return ReadSearchResult(dataReader);
        }

        return null;
    }

    public async Task<IEnumerable<SearchResult<AutoMergingMetadata>>> SearchLeafChunksAsync(
        ReadOnlyMemory<float> queryEmbedding,
        AutoMergingEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    )
    {
        var searchResults = new List<SearchResult<AutoMergingMetadata>>();
        var connection = await Npgsql.OpenConnectionAsync(cancellationToken);

        await using (connection)
        {
            await using var cmd = connection.CreateCommand();
            cmd.CommandText = $"""
                               SELECT *
                               FROM (
                                    SELECT id, text, metadata,
                                           1 - (embedding <=> @embedding) AS cosine_similarity
                                    FROM
                                        {Schema}.{Table}
                                    WHERE
                                        metadata ->> 'ParentIndex' IS NOT NULL AND
                                        metadata @> JSONB_BUILD_OBJECT(
                                            'EmbeddingOptions', @embeddingOptions
                                        )
                                    ) AS relevance_table
                               ORDER BY
                                   cosine_similarity DESC
                               LIMIT @limit
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
        }

        return searchResults;
    }
}