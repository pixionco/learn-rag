using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public record HypotheticalQuestionPreviewQuery(
    Guid DocumentId,
    HypotheticalQuestionEmbeddingOptions EmbeddingOptions,
    int Limit
) : IRequest<IEnumerable<EmbeddingRecord<HypotheticalQuestionMetadata>>>;

public class HypotheticalQuestionPreviewHandler(
    IHypotheticalQuestionStrategyService hypotheticalQuestionStrategyService
) : IRequestHandler<HypotheticalQuestionPreviewQuery,
    IEnumerable<EmbeddingRecord<HypotheticalQuestionMetadata>>>
{
    public Task<IEnumerable<EmbeddingRecord<HypotheticalQuestionMetadata>>> Handle(
        HypotheticalQuestionPreviewQuery request,
        CancellationToken cancellationToken
    )
    {
        return hypotheticalQuestionStrategyService.PreviewAsync(
            request.DocumentId,
            request.EmbeddingOptions,
            request.Limit,
            cancellationToken
        );
    }
}