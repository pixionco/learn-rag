using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public record BasicPreviewQuery(
    Guid DocumentId,
    BasicEmbeddingOptions EmbeddingOptions,
    int Limit
) : IRequest<IEnumerable<EmbeddingRecord<BasicMetadata>>>;

public class BasicPreviewHandler(
    IBasicStrategyService basicStrategyService
) : IRequestHandler<BasicPreviewQuery, IEnumerable<EmbeddingRecord<BasicMetadata>>>
{
    public Task<IEnumerable<EmbeddingRecord<BasicMetadata>>> Handle(
        BasicPreviewQuery request,
        CancellationToken cancellationToken
    )
    {
        return basicStrategyService.PreviewAsync(
            request.DocumentId,
            request.EmbeddingOptions,
            request.Limit,
            cancellationToken
        );
    }
}