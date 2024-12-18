using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IHierarchicalStrategyService : IStrategyService<HierarchicalMetadata, HierarchicalEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchAsync(
        string query,
        HierarchicalEmbeddingOptions embeddingOptions,
        HierarchicalRetrievalOptions retrievalOptions
    );

    Task EmbedAsync(
        Guid documentId,
        HierarchicalEmbeddingOptions embeddingOptions
    );
}