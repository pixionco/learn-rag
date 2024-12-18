using FluentValidation;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public class SentenceWindowPreviewQueryValidator : AbstractValidator<SentenceWindowPreviewQuery>
{
    public SentenceWindowPreviewQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(SentenceWindowPreviewQuery.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new SentenceWindowEmbeddingOptionsValidator());

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(SentenceWindowPreviewQuery.Limit)} must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage($"{nameof(SentenceWindowPreviewQuery.Limit)} must be less than or equal to 10");
    }
}