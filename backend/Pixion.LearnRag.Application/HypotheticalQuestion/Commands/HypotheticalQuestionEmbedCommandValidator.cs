using FluentValidation;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Commands;

public class HypotheticalQuestionEmbedCommandValidator : AbstractValidator<HypotheticalQuestionEmbedCommand>
{
    public HypotheticalQuestionEmbedCommandValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbedCommand.DocumentId)} can't be null, empty, or a whitespace."
            );

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HypotheticalQuestionEmbeddingOptionsValidator());
    }
}