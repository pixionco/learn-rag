using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Core.Interfaces;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Exceptions;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public abstract class StrategyService<TMetadata, TEmbeddingOptions>(
    // supports only generic strategy functions
    IStrategyRepository<TMetadata, TEmbeddingOptions> strategyRepository,
    IDocumentRepository documentRepository,
    IChunkingService chunkingService,
    IEmbeddingClient embeddingClient
) : IStrategyService<TMetadata, TEmbeddingOptions>
    where TMetadata : IMetadata
    where TEmbeddingOptions : struct, IEmbeddingOptions
{
    protected readonly IChunkingService ChunkingService = chunkingService;
    protected readonly IDocumentRepository DocumentRepository = documentRepository;
    protected readonly IEmbeddingClient EmbeddingClient = embeddingClient;

    public async Task<IEnumerable<TEmbeddingOptions>> GetDocumentEmbeddingOptionsAsync(
        Guid documentId,
        CancellationToken cancellationToken = default
    )
    {
        var document = await DocumentRepository.GetDocumentAsync(documentId, cancellationToken);
        if (document is null) throw new LRDocumentNotFoundException(documentId);

        return await strategyRepository.GetDocumentEmbeddingOptionsAsync(documentId, cancellationToken);
    }

    public async Task<IEnumerable<EmbeddingRecord<TMetadata>>> PreviewAsync(
        Guid documentId,
        TEmbeddingOptions embeddingOptions,
        int limit = 10,
        CancellationToken cancellationToken = default
    )
    {
        var document = await DocumentRepository.GetDocumentAsync(documentId, cancellationToken);
        if (document is null) throw new LRDocumentNotFoundException(documentId);

        return await strategyRepository.PreviewAsync(
            documentId,
            embeddingOptions,
            limit,
            cancellationToken
        );
    }

    protected async Task<Document> GetDocumentSafelyAsync(Guid documentId, TEmbeddingOptions embeddingOptions)
    {
        var alreadyEmbedded = await strategyRepository.GetDocumentEmbeddingOptionsAsync(documentId);
        if (alreadyEmbedded.Any(existingEmbeddingOptions => existingEmbeddingOptions.Equals(embeddingOptions)))
            throw new LRAlreadyEmbededException(Strategy.Hierarchical, documentId, embeddingOptions);

        var document = await DocumentRepository.GetDocumentAsync(documentId);
        if (document is null) throw new LRDocumentNotFoundException(documentId);

        return document;
    }

    protected async Task<IList<ReadOnlyMemory<float>>> EmbedSafelyAsync(
        IList<string> chunkList
    )
    {
        var chunkEmbeddingList = await EmbeddingClient.GenerateEmbeddingsAsync(chunkList);
        if (!chunkEmbeddingList.Ok)
            throw chunkEmbeddingList.Exception;

        if (chunkList.Count != chunkEmbeddingList.Value.Count)
            throw new LREmbeddingCountMismatchException(
                chunkList.Count,
                chunkEmbeddingList.Value.Count
            );

        return chunkEmbeddingList.Value;
    }
}