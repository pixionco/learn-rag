using FluentValidation;

namespace Pixion.LearnRag.UseCases.Basic.Commands;

public class BasicEmbedCommandValidator : AbstractValidator<BasicEmbedCommand>
{
    public BasicEmbedCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(BasicEmbedCommand.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new BasicEmbeddingOptionsValidator());
    }
}