using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Interfaces;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface IStrategyRepository<TMetadata, TEmbeddingOptions>
    where TMetadata : IMetadata
    where TEmbeddingOptions : struct, IEmbeddingOptions
{
    Task InsertAsync(EmbeddingRecord<TMetadata> embeddingRecord, CancellationToken cancellationToken = default);

    Task BatchInsertAsync(
        IEnumerable<EmbeddingRecord<TMetadata>> embeddingRecords,
        CancellationToken cancellationToken = default
    );

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

    public Task<IEnumerable<SearchResult<TMetadata>>> DefaultSearchAsync(
        ReadOnlyMemory<float> queryEmbedding,
        TEmbeddingOptions embeddingOptions,
        int limit,
        CancellationToken cancellationToken = default
    );
}