using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface IAutoMergingStrategyRepository : IStrategyRepository<AutoMergingMetadata, AutoMergingEmbeddingOptions>
{
    public Task<SearchResult<AutoMergingMetadata>?> GetParentChunkAsync(
        Guid documentId,
        int parentIndex,
        AutoMergingEmbeddingOptions embeddingOptions,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<SearchResult<AutoMergingMetadata>>> SearchLeafChunksAsync(
        ReadOnlyMemory<float> queryEmbedding,
        AutoMergingEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    );
}