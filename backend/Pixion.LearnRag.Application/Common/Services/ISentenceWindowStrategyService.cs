using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface
    ISentenceWindowStrategyService : IStrategyService<SentenceWindowMetadata, SentenceWindowEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> SearchAsync(
        string query,
        SentenceWindowEmbeddingOptions embeddingOptions,
        SentenceWindowRetrievalOptions retrievalOptions
    );

    Task EmbedAsync(
        Guid documentId,
        SentenceWindowEmbeddingOptions embeddingOptions
    );
}