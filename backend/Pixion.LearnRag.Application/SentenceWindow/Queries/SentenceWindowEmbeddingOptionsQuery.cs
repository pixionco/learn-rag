using MediatR;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public record SentenceWindowEmbeddingOptionsQuery(
    Guid DocumentId
) : IRequest<IEnumerable<SentenceWindowEmbeddingOptions>>;

public class SentenceWindowDocumentOptionsHandler(
    ISentenceWindowStrategyService sentenceWindowStrategy
) : IRequestHandler<SentenceWindowEmbeddingOptionsQuery, IEnumerable<SentenceWindowEmbeddingOptions>>
{
    public Task<IEnumerable<SentenceWindowEmbeddingOptions>> Handle(
        SentenceWindowEmbeddingOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return sentenceWindowStrategy
            .GetDocumentEmbeddingOptionsAsync(request.DocumentId, cancellationToken);
    }
}