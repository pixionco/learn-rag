using MediatR;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public record HypotheticalQuestionEmbeddingOptionsQuery(
    Guid DocumentId
) : IRequest<IEnumerable<HypotheticalQuestionEmbeddingOptions>>;

public class HypotheticalQuestionDocumentOptionsHandler(
    IHypotheticalQuestionStrategyService hypotheticalQuestionStrategy
) : IRequestHandler<HypotheticalQuestionEmbeddingOptionsQuery,
    IEnumerable<HypotheticalQuestionEmbeddingOptions>>
{
    public Task<IEnumerable<HypotheticalQuestionEmbeddingOptions>> Handle(
        HypotheticalQuestionEmbeddingOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return hypotheticalQuestionStrategy
            .GetDocumentEmbeddingOptionsAsync(request.DocumentId, cancellationToken);
    }
}