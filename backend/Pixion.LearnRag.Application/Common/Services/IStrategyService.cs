using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IStrategyService<TMetadata, TEmbeddingOptions>
    where TMetadata : IMetadata
    where TEmbeddingOptions : struct, IEmbeddingOptions
{
    public Task<IEnumerable<TEmbeddingOptions>> GetDocumentEmbeddingOptionsAsync(
        Guid documentId,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<EmbeddingRecord<TMetadata>>> PreviewAsync(
        Guid documentId,
        TEmbeddingOptions embeddingOptions,
        int limit = 10,
        CancellationToken cancellationToken = default
    );
}