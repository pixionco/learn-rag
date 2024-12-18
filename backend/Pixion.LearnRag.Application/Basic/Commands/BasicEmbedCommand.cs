using MediatR;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Basic.Commands;

public record BasicEmbedCommand(
    Guid DocumentId,
    BasicEmbeddingOptions EmbeddingOptions
) : IRequest;

public class BasicEmbedHandler(
    IBasicStrategyService basicStrategyService
) : IRequestHandler<BasicEmbedCommand>
{
    public async Task Handle(BasicEmbedCommand request, CancellationToken cancellationToken)
    {
        var (documentId, embeddingOptions) = request;

        await basicStrategyService.EmbedAsync(documentId, embeddingOptions);
    }
}