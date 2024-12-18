using FluentValidation;

namespace Pixion.LearnRag.UseCases.AutoMerging.Commands;

public class AutoMergingEmbedCommandValidator : AbstractValidator<AutoMergingEmbedCommand>
{
    public AutoMergingEmbedCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(AutoMergingEmbedCommand.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new AutoMergingEmbeddingOptionsValidator());
    }
}