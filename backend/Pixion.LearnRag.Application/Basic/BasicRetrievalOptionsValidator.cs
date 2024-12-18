using FluentValidation;
using Pixion.LearnRag.Core.Entities.Basic;

namespace Pixion.LearnRag.UseCases.Basic;

public class BasicRetrievalOptionsValidator : AbstractValidator<BasicRetrievalOptions>
{
    public BasicRetrievalOptionsValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(BasicRetrievalOptions.Limit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)6)
            .WithMessage($"{nameof(BasicRetrievalOptions.Limit)} must be less than or equal to 6.");
    }
}