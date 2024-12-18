using MediatR;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public record AutoMergingSearchQuery(
    string Query,
    AutoMergingEmbeddingOptions EmbeddingOptions,
    AutoMergingRetrievalOptions RetrievalOptions
) : IRequest<IEnumerable<SearchResult<AutoMergingMetadata>>>;

public class AutoMergingSearchHandler(IAutoMergingStrategyService autoMergingStrategyService)
    : IRequestHandler<AutoMergingSearchQuery, IEnumerable<SearchResult<AutoMergingMetadata>>>
{
    public Task<IEnumerable<SearchResult<AutoMergingMetadata>>> Handle(
        AutoMergingSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        return autoMergingStrategyService.SearchAsync(
            request.Query,
            request.EmbeddingOptions,
            request.RetrievalOptions
        );
    }
}