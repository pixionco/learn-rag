using MediatR;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public record HypotheticalQuestionSearchQuery(
    string Query,
    HypotheticalQuestionEmbeddingOptions EmbeddingOptions,
    HypotheticalQuestionRetrievalOptions RetrievalOptions
)
    : IRequest<IEnumerable<SearchResult<HypotheticalQuestionMetadata>>>;

public class HypotheticalQuestionSearchHandler(IHypotheticalQuestionStrategyService hypotheticalQuestionStrategyService)
    : IRequestHandler<HypotheticalQuestionSearchQuery, IEnumerable<SearchResult<HypotheticalQuestionMetadata>>>
{
    public Task<IEnumerable<SearchResult<HypotheticalQuestionMetadata>>> Handle(
        HypotheticalQuestionSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        return hypotheticalQuestionStrategyService.SearchAsync(
            request.Query,
            request.EmbeddingOptions,
            request.RetrievalOptions
        );
    }
}