using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface
    IHierarchicalStrategyRepository : IStrategyRepository<HierarchicalMetadata, HierarchicalEmbeddingOptions>
{
    public Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchByParentAsync(
        ReadOnlyMemory<float> queryEmbedding,
        Guid documentId,
        int parentIndex,
        HierarchicalEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchRootChunksAsync(
        ReadOnlyMemory<float> queryEmbedding,
        HierarchicalEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    );
}