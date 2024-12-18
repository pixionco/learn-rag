using System.Text.Json;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class SentenceWindowStrategyRepository(IOptions<VectorDatabaseConfig> config)
    : StrategyRepository<SentenceWindowMetadata, SentenceWindowEmbeddingOptions>(config, Strategy.SentenceWindow),
      ISentenceWindowStrategyRepository
{
    public async Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> GetNearbyChunksAsync(
        Guid documentId,
        SentenceWindowEmbeddingOptions embeddingOptions,
        int index,
        int range,
        CancellationToken cancellationToken = default
    )
    {
        var searchResults = new List<SearchResult<SentenceWindowMetadata>>();
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
                                   ) AND
                                   ABS((metadata->>'Index')::int - @index) BETWEEN 1 AND @range
                               """;
            cmd.Parameters.AddWithValue("@documentId", documentId);
            cmd.Parameters.AddWithValue(
                "@embeddingOptions",
                NpgsqlDbType.Jsonb,
                JsonSerializer.Serialize(embeddingOptions)
            );
            cmd.Parameters.AddWithValue("@index", index);
            cmd.Parameters.AddWithValue("@range", range);

            await using var dataReader = await cmd.ExecuteReaderAsync(cancellationToken);

            while (await dataReader.ReadAsync(cancellationToken)) searchResults.Add(ReadSearchResult(dataReader));
        }

        return searchResults;
    }
}