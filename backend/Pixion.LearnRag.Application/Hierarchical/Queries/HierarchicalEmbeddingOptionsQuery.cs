using MediatR;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public record HierarchicalEmbeddingOptionsQuery(
    Guid DocumentId
) : IRequest<IEnumerable<HierarchicalEmbeddingOptions>>;

public class HierarchicalDocumentOptionsHandler(
    IHierarchicalStrategyService hierarchicalStrategy
) : IRequestHandler<HierarchicalEmbeddingOptionsQuery, IEnumerable<HierarchicalEmbeddingOptions>>
{
    public Task<IEnumerable<HierarchicalEmbeddingOptions>> Handle(
        HierarchicalEmbeddingOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return hierarchicalStrategy
            .GetDocumentEmbeddingOptionsAsync(request.DocumentId, cancellationToken);
    }
}