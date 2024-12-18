using MediatR;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public record BasicEmbeddingOptionsQuery(
    Guid DocumentId
) : IRequest<IEnumerable<BasicEmbeddingOptions>>;

public class BasicDocumentOptionsHandler(
    IBasicStrategyService basicStrategy
) : IRequestHandler<BasicEmbeddingOptionsQuery, IEnumerable<BasicEmbeddingOptions>>
{
    public Task<IEnumerable<BasicEmbeddingOptions>> Handle(
        BasicEmbeddingOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return basicStrategy.GetDocumentEmbeddingOptionsAsync(request.DocumentId, cancellationToken);
    }
}