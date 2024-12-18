using FluentValidation;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public class HypotheticalQuestionPreviewQueryValidator : AbstractValidator<HypotheticalQuestionPreviewQuery>
{
    public HypotheticalQuestionPreviewQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(HypotheticalQuestionPreviewQuery.DocumentId)} can't be null, empty, or a whitespace."
            );

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HypotheticalQuestionEmbeddingOptionsValidator());

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(HypotheticalQuestionPreviewQuery.Limit)} must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage($"{nameof(HypotheticalQuestionPreviewQuery.Limit)} must be less than or equal to 10");
    }
}