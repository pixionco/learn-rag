using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface
    IHypotheticalQuestionStrategyService : IStrategyService<HypotheticalQuestionMetadata,
    HypotheticalQuestionEmbeddingOptions>
{
    Task<IEnumerable<SearchResult<HypotheticalQuestionMetadata>>> SearchAsync(
        string query,
        HypotheticalQuestionEmbeddingOptions embeddingOptions,
        HypotheticalQuestionRetrievalOptions retrievalOptions
    );

    Task EmbedAsync(
        Guid documentId,
        HypotheticalQuestionEmbeddingOptions embeddingOptions
    );
}