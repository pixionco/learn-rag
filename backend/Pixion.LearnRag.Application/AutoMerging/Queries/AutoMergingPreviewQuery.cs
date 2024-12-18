using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public record AutoMergingPreviewQuery(
    Guid DocumentId,
    AutoMergingEmbeddingOptions EmbeddingOptions,
    int Limit
) : IRequest<IEnumerable<EmbeddingRecord<AutoMergingMetadata>>>;

public class AutoMergingPreviewHandler(
    IAutoMergingStrategyService autoMergingStrategyService
) : IRequestHandler<AutoMergingPreviewQuery, IEnumerable<EmbeddingRecord<AutoMergingMetadata>>>
{
    public Task<IEnumerable<EmbeddingRecord<AutoMergingMetadata>>> Handle(
        AutoMergingPreviewQuery request,
        CancellationToken cancellationToken
    )
    {
        return autoMergingStrategyService.PreviewAsync(
            request.DocumentId,
            request.EmbeddingOptions,
            request.Limit,
            cancellationToken
        );
    }
}