using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public class SentenceWindowStrategyService(
    ISentenceWindowStrategyRepository sentenceWindowStrategyRepository,
    IEmbeddingClient embeddingClient,
    IChunkingService chunkingService,
    IDocumentRepository documentRepository
) : StrategyService<SentenceWindowMetadata, SentenceWindowEmbeddingOptions>(
        sentenceWindowStrategyRepository,
        documentRepository,
        chunkingService,
        embeddingClient
    ),
    ISentenceWindowStrategyService
{
    public async Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> SearchAsync(
        string query,
        SentenceWindowEmbeddingOptions embeddingOptions,
        SentenceWindowRetrievalOptions retrievalOptions
    )
    {
        var embeddingResult = await EmbeddingClient.GenerateEmbeddingAsync(query);
        if (!embeddingResult.Ok)
            throw embeddingResult.Exception;

        var results =
            await sentenceWindowStrategyRepository.DefaultSearchAsync(
                embeddingResult.Value,
                embeddingOptions,
                retrievalOptions.Limit
            );

        var items = new List<SearchResult<SentenceWindowMetadata>>();
        foreach (var result in results)
        {
            var nearby = (await sentenceWindowStrategyRepository.GetNearbyChunksAsync(
                result.Metadata.DocumentId,
                embeddingOptions,
                result.Metadata.Index,
                retrievalOptions.Range
            )).ToList();
            var before = nearby
                .Where(x => x.Metadata.Index < result.Metadata.Index)
                .OrderBy(x => x.Metadata.Index)
                .Select(x => x.Text)
                .ToList();
            var after = nearby
                .Where(x => x.Metadata.Index > result.Metadata.Index)
                .OrderBy(x => x.Metadata.Index)
                .Select(x => x.Text)
                .ToList();

            items.Add(result with { Text = string.Join("", before) + result.Text + string.Join("", after) });
        }

        return items
            .OrderBy(x => x.Metadata.DocumentId)
            .ThenBy(x => x.Metadata.Index);
    }

    public async Task EmbedAsync(Guid documentId, SentenceWindowEmbeddingOptions embeddingOptions)
    {
        var document = await GetDocumentSafelyAsync(documentId, embeddingOptions);

        var chunkList = ChunkingService.ChunkText(
                document.Text,
                embeddingOptions.ChunkSize,
                0
            )
            .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
            .ToList();
        var chunkEmbeddingList = await EmbedSafelyAsync(chunkList);

        var embeddingRecords = new List<EmbeddingRecord<SentenceWindowMetadata>>();
        for (var i = 0; i < chunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<SentenceWindowMetadata>(
                    Guid.NewGuid(),
                    chunkList[i],
                    chunkEmbeddingList[i],
                    new SentenceWindowMetadata(document.Id, i, embeddingOptions, chunkList[i])
                )
            );

        await sentenceWindowStrategyRepository.BatchInsertAsync(embeddingRecords);
    }
}