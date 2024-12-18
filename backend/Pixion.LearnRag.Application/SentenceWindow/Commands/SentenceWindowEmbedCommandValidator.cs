using FluentValidation;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Commands;

public class SentenceWindowEmbedCommandValidator : AbstractValidator<SentenceWindowEmbedCommand>
{
    public SentenceWindowEmbedCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(SentenceWindowEmbedCommand.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new SentenceWindowEmbeddingOptionsValidator());
    }
}