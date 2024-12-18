using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public class BasicStrategyService(
    IBasicStrategyRepository basicStrategyRepository,
    IEmbeddingClient embeddingClient,
    IChunkingService chunkingService,
    IDocumentRepository documentRepository
) : StrategyService<BasicMetadata, BasicEmbeddingOptions>(
        basicStrategyRepository,
        documentRepository,
        chunkingService,
        embeddingClient
    ),
    IBasicStrategyService
{
    public async Task<IEnumerable<SearchResult<BasicMetadata>>> SearchAsync(
        string query,
        BasicEmbeddingOptions embeddingOptions,
        BasicRetrievalOptions retrievalOptions
    )
    {
        var embeddingResult = await EmbeddingClient.GenerateEmbeddingAsync(query);
        if (!embeddingResult.Ok)
            throw embeddingResult.Exception;

        var results =
            await basicStrategyRepository.DefaultSearchAsync(
                embeddingResult.Value,
                embeddingOptions,
                retrievalOptions.Limit
            );

        return results
            .OrderBy(x => x.Metadata.DocumentId)
            .ThenBy(x => x.Metadata.Index);
    }

    public async Task EmbedAsync(Guid documentId, BasicEmbeddingOptions embeddingOptions)
    {
        var document = await GetDocumentSafelyAsync(documentId, embeddingOptions);

        var chunkList = ChunkingService.ChunkText(
                document.Text,
                embeddingOptions.ChunkSize,
                embeddingOptions.ChunkOverlap
            )
            .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
            .ToList();
        var chunkEmbeddingList = await EmbedSafelyAsync(chunkList);

        var embeddingRecords = new List<EmbeddingRecord<BasicMetadata>>();
        for (var i = 0; i < chunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<BasicMetadata>(
                    Guid.NewGuid(),
                    chunkList[i],
                    chunkEmbeddingList[i],
                    new BasicMetadata(
                        document.Id,
                        i,
                        embeddingOptions
                    )
                )
            );

        await basicStrategyRepository.BatchInsertAsync(embeddingRecords);
    }
}