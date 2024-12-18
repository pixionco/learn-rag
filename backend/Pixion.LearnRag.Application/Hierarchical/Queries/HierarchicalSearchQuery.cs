using MediatR;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public record HierarchicalSearchQuery(
    string Query,
    HierarchicalEmbeddingOptions EmbeddingOptions,
    HierarchicalRetrievalOptions RetrievalOptions
) : IRequest<IEnumerable<SearchResult<HierarchicalMetadata>>>;

public class HierarchicalSearchHandler(IHierarchicalStrategyService hierarchicalStrategyService)
    : IRequestHandler<HierarchicalSearchQuery, IEnumerable<SearchResult<HierarchicalMetadata>>>
{
    public Task<IEnumerable<SearchResult<HierarchicalMetadata>>> Handle(
        HierarchicalSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        return hierarchicalStrategyService.SearchAsync(
            request.Query,
            request.EmbeddingOptions,
            request.RetrievalOptions
        );
    }
}