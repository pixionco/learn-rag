using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface
    ISentenceWindowStrategyRepository : IStrategyRepository<SentenceWindowMetadata, SentenceWindowEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> GetNearbyChunksAsync(
        Guid documentId,
        SentenceWindowEmbeddingOptions embeddingOptions,
        int index,
        int range,
        CancellationToken cancellationToken = default
    );
}