using FluentValidation;
using Pixion.LearnRag.Core.Entities.AutoMerging;

namespace Pixion.LearnRag.UseCases.AutoMerging;

public class AutoMergingRetrievalOptionsValidator : AbstractValidator<AutoMergingRetrievalOptions>
{
    public AutoMergingRetrievalOptionsValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(AutoMergingRetrievalOptions.Limit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)6)
            .WithMessage($"{nameof(AutoMergingRetrievalOptions.Limit)} must be less than or equal to 6.");

        RuleFor(x => x.ChildParentPrevalenceFactor)
            .GreaterThanOrEqualTo(0.4)
            .WithMessage(
                $"{nameof(AutoMergingRetrievalOptions.ChildParentPrevalenceFactor)} must be greater than or equal to 0.4"
            )
            .LessThanOrEqualTo(1)
            .WithMessage(
                $"{nameof(AutoMergingRetrievalOptions.ChildParentPrevalenceFactor)} must be less than or equal to 1."
            );
    }
}