using MediatR;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public record AutoMergingEmbeddingOptionsQuery(
    Guid DocumentId
) : IRequest<IEnumerable<AutoMergingEmbeddingOptions>>;

public class AutoMergingDocumentOptionsHandler(
    IAutoMergingStrategyService autoMergingStrategy
) : IRequestHandler<AutoMergingEmbeddingOptionsQuery, IEnumerable<AutoMergingEmbeddingOptions>>
{
    public Task<IEnumerable<AutoMergingEmbeddingOptions>> Handle(
        AutoMergingEmbeddingOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        return autoMergingStrategy
            .GetDocumentEmbeddingOptionsAsync(request.DocumentId, cancellationToken);
    }
}