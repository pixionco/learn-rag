using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IAutoMergingStrategyService : IStrategyService<AutoMergingMetadata, AutoMergingEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<AutoMergingMetadata>>> SearchAsync(
        string query,
        AutoMergingEmbeddingOptions embeddingOptions,
        AutoMergingRetrievalOptions retrievalOptions
    );

    Task EmbedAsync(
        Guid documentId,
        AutoMergingEmbeddingOptions embeddingOptions
    );
}