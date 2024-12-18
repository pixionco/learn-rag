using MediatR;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Hierarchical.Commands;

public record HierarchicalEmbedCommand(
    Guid DocumentId,
    HierarchicalEmbeddingOptions EmbeddingOptions
) : IRequest;

public class HierarchicalEmbedHandler(IHierarchicalStrategyService hierarchicalStrategyService)
    : IRequestHandler<HierarchicalEmbedCommand>
{
    public async Task Handle(HierarchicalEmbedCommand request, CancellationToken cancellationToken)
    {
        var (documentId, embeddingOptions) = request;

        await hierarchicalStrategyService.EmbedAsync(documentId, embeddingOptions);
    }
}