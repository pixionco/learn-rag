using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public class HierarchicalStrategyService(
    IHierarchicalStrategyRepository hierarchicalStrategyRepository,
    ISummaryGenerationService summaryGenerationService,
    IEmbeddingClient embeddingClient,
    IChunkingService chunkingService,
    IDocumentRepository documentRepository
) : StrategyService<HierarchicalMetadata, HierarchicalEmbeddingOptions>(
        hierarchicalStrategyRepository,
        documentRepository,
        chunkingService,
        embeddingClient
    ),
    IHierarchicalStrategyService
{
    public async Task<IEnumerable<SearchResult<HierarchicalMetadata>>> SearchAsync(
        string query,
        HierarchicalEmbeddingOptions embeddingOptions,
        HierarchicalRetrievalOptions retrievalOptions
    )
    {
        var embeddingResult = await EmbeddingClient.GenerateEmbeddingAsync(query);
        if (!embeddingResult.Ok)
            throw embeddingResult.Exception;

        var parentResults =
            (await hierarchicalStrategyRepository.SearchRootChunksAsync(
                embeddingResult.Value,
                embeddingOptions,
                retrievalOptions.Limit
            ))
            .ToList();

        var results = new List<SearchResult<HierarchicalMetadata>>();
        foreach (var parentResult in parentResults)
        {
            var childResults = await hierarchicalStrategyRepository.SearchByParentAsync(
                embeddingResult.Value,
                parentResult.Metadata.DocumentId,
                parentResult.Metadata.Index,
                embeddingOptions,
                retrievalOptions.ChildLimit
            );
            results.AddRange(childResults);
        }

        return results
            .OrderBy(x => x.Metadata.DocumentId)
            .ThenBy(x => x.Metadata.Index);
    }

    public async Task EmbedAsync(Guid documentId, HierarchicalEmbeddingOptions embeddingOptions)
    {
        var document = await GetDocumentSafelyAsync(documentId, embeddingOptions);

        var summaryChunkList =
            ChunkingService.ChunkText(document.Text, embeddingOptions.ChunkSize, embeddingOptions.ChunkOverlap)
                .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
                .ToList();

        // TODO: batch generate summaries
        var summaryList = new List<string>();
        foreach (var summaryChunk in summaryChunkList)
        {
            var summary =
                await summaryGenerationService.GenerateSummaryAsync(summaryChunk);
            if (!summary.Ok) throw summary.Exception;

            summaryList.Add(summary.Value);
        }

        var summaryEmbeddingsList = await EmbedSafelyAsync(summaryList);

        var embeddingRecords = new List<EmbeddingRecord<HierarchicalMetadata>>();
        for (var i = 0; i < summaryChunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<HierarchicalMetadata>(
                    Guid.NewGuid(),
                    summaryChunkList[i],
                    summaryEmbeddingsList[i],
                    new HierarchicalMetadata(
                        document.Id,
                        i,
                        null,
                        summaryList[i],
                        embeddingOptions
                    )
                )
            );

        var childStrings = summaryChunkList
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

        var childStringsEmbeddings = await EmbedSafelyAsync(
            childStrings.Select(t => t.Text).ToList()
        );

        for (var i = 0; i < childStrings.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<HierarchicalMetadata>(
                    Guid.NewGuid(),
                    childStrings[i].Text,
                    childStringsEmbeddings[i],
                    new HierarchicalMetadata(
                        document.Id,
                        i,
                        childStrings[i].ParentIndex,
                        null,
                        embeddingOptions
                    )
                )
            );

        await hierarchicalStrategyRepository.BatchInsertAsync(embeddingRecords);
    }
}