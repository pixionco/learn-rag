using MediatR;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.AutoMerging.Commands;

public record AutoMergingEmbedCommand(
    Guid DocumentId,
    AutoMergingEmbeddingOptions EmbeddingOptions
)
    : IRequest;

public class AutoMergingEmbedHandler(IAutoMergingStrategyService autoMergingStrategyService)
    : IRequestHandler<AutoMergingEmbedCommand>
{
    public async Task Handle(AutoMergingEmbedCommand request, CancellationToken cancellationToken)
    {
        var (documentId, embeddingOptions) = request;

        await autoMergingStrategyService.EmbedAsync(documentId, embeddingOptions);
    }
}