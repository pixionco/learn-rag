using MediatR;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public record BasicSearchQuery(
    string Query,
    BasicEmbeddingOptions EmbeddingOptions,
    BasicRetrievalOptions RetrievalOptions
) : IRequest<IEnumerable<SearchResult<BasicMetadata>>>;

public class BasicSearchHandler(
    IBasicStrategyService basicStrategyService
) : IRequestHandler<BasicSearchQuery, IEnumerable<SearchResult<BasicMetadata>>>
{
    public Task<IEnumerable<SearchResult<BasicMetadata>>> Handle(
        BasicSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        return basicStrategyService.SearchAsync(
            request.Query,
            request.EmbeddingOptions,
            request.RetrievalOptions
        );
    }
}