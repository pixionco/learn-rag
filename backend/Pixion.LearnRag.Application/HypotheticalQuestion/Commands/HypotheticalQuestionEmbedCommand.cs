using MediatR;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Commands;

public record HypotheticalQuestionEmbedCommand(
    Guid DocumentId,
    HypotheticalQuestionEmbeddingOptions EmbeddingOptions
)
    : IRequest;

public class HypotheticalQuestionEmbedHandler(IHypotheticalQuestionStrategyService hypotheticalQuestionStrategyService)
    : IRequestHandler<HypotheticalQuestionEmbedCommand>
{
    public async Task Handle(HypotheticalQuestionEmbedCommand request, CancellationToken cancellationToken)
    {
        var (documentId, embeddingOptions) = request;

        await hypotheticalQuestionStrategyService.EmbedAsync(documentId, embeddingOptions);
    }
}