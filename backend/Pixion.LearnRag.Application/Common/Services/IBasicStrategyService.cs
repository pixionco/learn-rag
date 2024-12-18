using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IBasicStrategyService : IStrategyService<BasicMetadata, BasicEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<BasicMetadata>>> SearchAsync(
        string query,
        BasicEmbeddingOptions embeddingOptions,
        BasicRetrievalOptions retrievalOptions
    );

    Task EmbedAsync(
        Guid documentId,
        BasicEmbeddingOptions embeddingOptions
    );
}