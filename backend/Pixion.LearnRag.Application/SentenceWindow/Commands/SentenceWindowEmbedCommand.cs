using MediatR;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Commands;

public record SentenceWindowEmbedCommand(
    Guid DocumentId,
    SentenceWindowEmbeddingOptions EmbeddingOptions
)
    : IRequest;

public class SentenceWindowEmbedHandler(
    ISentenceWindowStrategyService sentenceWindowStrategyService
) : IRequestHandler<SentenceWindowEmbedCommand>
{
    public async Task Handle(SentenceWindowEmbedCommand request, CancellationToken cancellationToken)
    {
        var (documentId, embeddingOptions) = request;

        await sentenceWindowStrategyService.EmbedAsync(documentId, embeddingOptions);
    }
}