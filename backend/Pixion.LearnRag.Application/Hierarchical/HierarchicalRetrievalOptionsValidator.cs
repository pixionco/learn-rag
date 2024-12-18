using FluentValidation;
using Pixion.LearnRag.Core.Entities.Hierarchical;

namespace Pixion.LearnRag.UseCases.Hierarchical;

public class HierarchicalRetrievalOptionsValidator : AbstractValidator<HierarchicalRetrievalOptions>
{
    public HierarchicalRetrievalOptionsValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HierarchicalRetrievalOptions.Limit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2)
            .WithMessage($"{nameof(HierarchicalRetrievalOptions.Limit)} must be less than or equal to 2.");

        RuleFor(x => x.ChildLimit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HierarchicalRetrievalOptions.ChildLimit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)6)
            .WithMessage($"{nameof(HierarchicalRetrievalOptions.ChildLimit)} must be less than or equal to 6.");
    }
}