using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public record HierarchicalPreviewQuery(
    Guid DocumentId,
    HierarchicalEmbeddingOptions EmbeddingOptions,
    int Limit
) : IRequest<IEnumerable<EmbeddingRecord<HierarchicalMetadata>>>;

public class HierarchicalPreviewHandler(
    IHierarchicalStrategyService hierarchicalStrategyService
) : IRequestHandler<HierarchicalPreviewQuery, IEnumerable<EmbeddingRecord<HierarchicalMetadata>>>
{
    public Task<IEnumerable<EmbeddingRecord<HierarchicalMetadata>>> Handle(
        HierarchicalPreviewQuery request,
        CancellationToken cancellationToken
    )
    {
        return hierarchicalStrategyService.PreviewAsync(
            request.DocumentId,
            request.EmbeddingOptions,
            request.Limit,
            cancellationToken
        );
    }
}