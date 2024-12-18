using MediatR;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public record SentenceWindowSearchQuery(
    string Query,
    SentenceWindowEmbeddingOptions EmbeddingOptions,
    SentenceWindowRetrievalOptions RetrievalOptions
) : IRequest<IEnumerable<SearchResult<SentenceWindowMetadata>>>;

public class SentenceWindowSearchHandler(
    ISentenceWindowStrategyService sentenceWindowStrategyService
) : IRequestHandler<SentenceWindowSearchQuery, IEnumerable<SearchResult<SentenceWindowMetadata>>>
{
    public Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> Handle(
        SentenceWindowSearchQuery request,
        CancellationToken cancellationToken
    )
    {
        return sentenceWindowStrategyService.SearchAsync(
            request.Query,
            request.EmbeddingOptions,
            request.RetrievalOptions
        );
    }
}