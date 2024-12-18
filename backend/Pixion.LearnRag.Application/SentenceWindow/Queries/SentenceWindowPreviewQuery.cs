using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public record SentenceWindowPreviewQuery(
    Guid DocumentId,
    SentenceWindowEmbeddingOptions EmbeddingOptions,
    int Limit
) : IRequest<IEnumerable<EmbeddingRecord<SentenceWindowMetadata>>>;

public class SentenceWindowPreviewHandler(
    ISentenceWindowStrategyService hypotheticalQuestionStrategyService
) : IRequestHandler<SentenceWindowPreviewQuery,
    IEnumerable<EmbeddingRecord<SentenceWindowMetadata>>>
{
    public Task<IEnumerable<EmbeddingRecord<SentenceWindowMetadata>>> Handle(
        SentenceWindowPreviewQuery request,
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