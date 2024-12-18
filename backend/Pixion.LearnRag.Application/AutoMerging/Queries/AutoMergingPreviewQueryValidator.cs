using FluentValidation;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public class AutoMergingPreviewQueryValidator : AbstractValidator<AutoMergingPreviewQuery>
{
    public AutoMergingPreviewQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(AutoMergingPreviewQuery.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new AutoMergingEmbeddingOptionsValidator());

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(AutoMergingPreviewQuery.Limit)} must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage($"{nameof(AutoMergingPreviewQuery.Limit)} must be less than or equal to 10");
    }
}