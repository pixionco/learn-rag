using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public class AutoMergingStrategyService(
    IAutoMergingStrategyRepository autoMergingStrategyRepository,
    IEmbeddingClient embeddingClient,
    IChunkingService chunkingService,
    IDocumentRepository documentRepository
) : StrategyService<AutoMergingMetadata, AutoMergingEmbeddingOptions>(
        autoMergingStrategyRepository,
        documentRepository,
        chunkingService,
        embeddingClient
    ),
    IAutoMergingStrategyService
{
    public async Task<IEnumerable<SearchResult<AutoMergingMetadata>>> SearchAsync(
        string query,
        AutoMergingEmbeddingOptions embeddingOptions,
        AutoMergingRetrievalOptions retrievalOptions
    )
    {
        var embeddingResult = await EmbeddingClient.GenerateEmbeddingAsync(query);
        if (!embeddingResult.Ok)
            throw embeddingResult.Exception;

        var childResults =
            (await autoMergingStrategyRepository.SearchLeafChunksAsync(
                embeddingResult.Value,
                embeddingOptions,
                retrievalOptions.Limit
            ))
            .ToList();

        if (childResults.Count == 0) return childResults;

        var n = (int)Math.Ceiling(
            embeddingOptions.ChunkSize / Convert.ToDouble(embeddingOptions.ChildChunkSize) *
            retrievalOptions.ChildParentPrevalenceFactor
        );
        var allSearchResults = new List<SearchResult<AutoMergingMetadata>>();

        var nonMergedChunks = childResults
            .GroupBy(el => new { el.Metadata.DocumentId, el.Metadata.ParentIndex })
            .Where(group => group.Count() < n)
            .SelectMany(group => group);
        allSearchResults.AddRange(nonMergedChunks);

        var mergeableGroups = childResults
            .GroupBy(el => new { el.Metadata.DocumentId, el.Metadata.ParentIndex })
            .Where(group => group.Count() >= n);
        foreach (var mergeableGroup in mergeableGroups)
        {
            var parentChunk =
                await autoMergingStrategyRepository.GetParentChunkAsync(
                    mergeableGroup.Key.DocumentId,
                    mergeableGroup.Key.ParentIndex!.Value, // should always have value on child chunks
                    embeddingOptions
                );
            if (parentChunk != null) allSearchResults.Add(parentChunk);
        }

        return allSearchResults
            .OrderBy(x => x.Metadata.DocumentId)
            .ThenBy(x => x.Metadata.Index);
    }

    public async Task EmbedAsync(Guid documentId, AutoMergingEmbeddingOptions embeddingOptions)
    {
        var document = await GetDocumentSafelyAsync(documentId, embeddingOptions);

        var parentChunkList = ChunkingService.ChunkText(
                document.Text,
                embeddingOptions.ChunkSize,
                embeddingOptions.ChunkOverlap
            )
            .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
            .ToList();

        var parentChunkEmbeddingList = await EmbedSafelyAsync(parentChunkList);

        var embeddingRecords = new List<EmbeddingRecord<AutoMergingMetadata>>();
        for (var i = 0; i < parentChunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<AutoMergingMetadata>(
                    Guid.NewGuid(),
                    parentChunkList[i],
                    parentChunkEmbeddingList[i],
                    new AutoMergingMetadata(
                        document.Id,
                        i,
                        null,
                        embeddingOptions
                    )
                )
            );

        var childChunkList = parentChunkList
            .SelectMany(
                (parentChunk, parentIndex) =>
                    ChunkingService.ChunkText(
                            parentChunk,
                            embeddingOptions.ChildChunkSize,
                            embeddingOptions.ChildChunkOverlap
                        )
                        .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
                        .Select(
                            childChunk => new
                            {
                                Text = childChunk,
                                ParentIndex = parentIndex
                            }
                        )
            )
            .ToList();

        var childChunkEmbeddingList = await EmbedSafelyAsync(
            childChunkList.Select(t => t.Text).ToList()
        );

        for (var i = 0; i < childChunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<AutoMergingMetadata>(
                    Guid.NewGuid(),
                    childChunkList[i].Text,
                    childChunkEmbeddingList[i],
                    new AutoMergingMetadata(
                        document.Id,
                        i,
                        childChunkList[i].ParentIndex,
                        embeddingOptions
                    )
                )
            );

        await autoMergingStrategyRepository.BatchInsertAsync(embeddingRecords);
    }
}