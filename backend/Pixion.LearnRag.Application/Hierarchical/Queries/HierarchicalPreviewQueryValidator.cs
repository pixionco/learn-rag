using FluentValidation;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public class HierarchicalPreviewQueryValidator : AbstractValidator<HierarchicalPreviewQuery>
{
    public HierarchicalPreviewQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(HierarchicalPreviewQuery.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HierarchicalEmbeddingOptionsValidator());

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(HierarchicalPreviewQuery.Limit)} must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage($"{nameof(HierarchicalPreviewQuery.Limit)} must be less than or equal to 10");
    }
}