using FluentValidation;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public class HierarchicalSearchQueryValidator : AbstractValidator<HierarchicalSearchQuery>
{
    public HierarchicalSearchQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage($"{nameof(HierarchicalSearchQuery.Query)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HierarchicalEmbeddingOptionsValidator());
        RuleFor(x => x.RetrievalOptions).SetValidator(new HierarchicalRetrievalOptionsValidator());
    }
}