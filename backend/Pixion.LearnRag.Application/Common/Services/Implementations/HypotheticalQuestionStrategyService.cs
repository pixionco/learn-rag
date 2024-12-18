using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Common.Services.Implementations;

public class HypotheticalQuestionStrategyService(
    IHypotheticalQuestionStrategyRepository hypotheticalQuestionStrategyRepository,
    IQuestionGenerationService questionGenerationService,
    IEmbeddingClient embeddingClient,
    IChunkingService chunkingService,
    IDocumentRepository documentRepository
) : StrategyService<HypotheticalQuestionMetadata, HypotheticalQuestionEmbeddingOptions>(
        hypotheticalQuestionStrategyRepository,
        documentRepository,
        chunkingService,
        embeddingClient
    ),
    IHypotheticalQuestionStrategyService
{
    public async Task<IEnumerable<SearchResult<HypotheticalQuestionMetadata>>> SearchAsync(
        string query,
        HypotheticalQuestionEmbeddingOptions embeddingOptions,
        HypotheticalQuestionRetrievalOptions retrievalOptions
    )
    {
        var embeddingResult = await EmbeddingClient.GenerateEmbeddingAsync(query);
        if (!embeddingResult.Ok)
            throw embeddingResult.Exception;

        var results =
            await hypotheticalQuestionStrategyRepository.DefaultSearchAsync(
                embeddingResult.Value,
                embeddingOptions,
                retrievalOptions.Limit
            );

        return results
            .OrderBy(x => x.Metadata.DocumentId)
            .ThenBy(x => x.Metadata.Index);
    }

    public async Task EmbedAsync(Guid documentId, HypotheticalQuestionEmbeddingOptions embeddingOptions)
    {
        var document = await GetDocumentSafelyAsync(documentId, embeddingOptions);

        var chunkList =
            ChunkingService.ChunkText(document.Text, embeddingOptions.ChunkSize, embeddingOptions.ChunkOverlap)
                .Where(chunk => !string.IsNullOrWhiteSpace(chunk))
                .ToList();

        // TODO: batch generate questions
        var chunkQuestionsList = new List<IEnumerable<string>>();
        foreach (var chunk in chunkList)
        {
            var questions =
                await questionGenerationService.GenerateQuestionsAsync(
                    chunk,
                    embeddingOptions.NumberOfQuestions
                );
            if (!questions.Ok) throw questions.Exception;

            chunkQuestionsList.Add(questions.Value);
        }

        // Question generation returns empty array ([]) when it can't generate any meaningful questions
        // from the provided chunk, such chunks are useless for this strategy so we discard them
        chunkList = chunkList
            .Where((chunk, index) => chunkQuestionsList[index].Any())
            .ToList();
        chunkQuestionsList = chunkQuestionsList
            .Where(questions => questions.Any())
            .ToList();

        var chunkQuestionsEmbeddingList = await EmbedSafelyAsync(
            chunkQuestionsList.Select(questions => string.Join('\n', questions) ?? "").ToList()
        );

        var embeddingRecords = new List<EmbeddingRecord<HypotheticalQuestionMetadata>>();
        for (var i = 0; i < chunkList.Count; i++)
            embeddingRecords.Add(
                new EmbeddingRecord<HypotheticalQuestionMetadata>(
                    Guid.NewGuid(),
                    chunkList[i],
                    chunkQuestionsEmbeddingList[i],
                    new HypotheticalQuestionMetadata(
                        document.Id,
                        i,
                        embeddingOptions,
                        chunkQuestionsList[i]
                    )
                )
            );

        await hypotheticalQuestionStrategyRepository.BatchInsertAsync(embeddingRecords);
    }
}