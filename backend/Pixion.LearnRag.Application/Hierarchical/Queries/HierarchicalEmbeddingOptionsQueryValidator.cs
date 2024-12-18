using FluentValidation;

namespace Pixion.LearnRag.UseCases.Hierarchical.Queries;

public class HierarchicalEmbeddingOptionsQueryValidator : AbstractValidator<HierarchicalEmbeddingOptionsQuery>
{
    public HierarchicalEmbeddingOptionsQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(HierarchicalEmbeddingOptionsQuery.DocumentId)} can't be null, empty, or a whitespace."
            );
    }
}