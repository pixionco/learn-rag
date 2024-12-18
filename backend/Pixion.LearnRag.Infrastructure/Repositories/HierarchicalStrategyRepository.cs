using System.Text.Json;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Pgvector;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class HierarchicalStrategyRepository(IOptions<VectorDatabaseConfig> config)
    : StrategyRepository<HierarchicalMetadata, HierarchicalEmbeddingOptions>(config, Strategy.Hierarchical),
      IHierarchicalStrategyRepository
{
    public async Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchByParentAsync(
        ReadOnlyMemory<float> queryEmbedding,
        Guid documentId,
        int parentIndex,
        HierarchicalEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken
    )
    {
        var searchResults = new List<SearchResult<HierarchicalMetadata>>();
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
                                            'EmbeddingOptions', @embeddingOptions,
                                            'ParentIndex', @parentIndex,
                                            'DocumentId', @documentId
                                        )
                                    ) AS relevance_table
                               ORDER BY
                                   cosine_similarity DESC
                               LIMIT @limit
                               """;
            cmd.Parameters.AddWithValue("@embedding", new Vector(queryEmbedding));
            cmd.Parameters.AddWithValue("@documentId", documentId);
            cmd.Parameters.AddWithValue("@parentIndex", parentIndex);
            cmd.Parameters.AddWithValue(
                "@embeddingOptions",
                NpgsqlDbType.Jsonb,
                JsonSerializer.Serialize(embeddingOptions)
            );
            cmd.Parameters.AddWithValue("@limit", limit);

            await using var dataReader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await dataReader.ReadAsync(cancellationToken)) searchResults.Add(ReadSearchResult(dataReader));
        }

        return searchResults;
    }

    public async Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchRootChunksAsync(
        ReadOnlyMemory<float> queryEmbedding,
        HierarchicalEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken
    )
    {
        var searchResults = new List<SearchResult<HierarchicalMetadata>>();
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
                                        metadata ? 'ParentIndex' AND
                                        metadata @> jsonb_build_object(
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