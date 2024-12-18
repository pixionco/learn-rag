using FluentValidation;
using Pixion.LearnRag.Core.Entities.SentenceWindow;

namespace Pixion.LearnRag.UseCases.SentenceWindow;

public class SentenceWindowRetrievalOptionsValidator : AbstractValidator<SentenceWindowRetrievalOptions>
{
    public SentenceWindowRetrievalOptionsValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(SentenceWindowRetrievalOptions.Limit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)6)
            .WithMessage($"{nameof(SentenceWindowRetrievalOptions.Limit)} must be less than or equal to 6.");

        RuleFor(x => x.Range)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(SentenceWindowRetrievalOptions.Range)} must be greater than or equal to 0.")
            .LessThanOrEqualTo((ushort)3)
            .WithMessage($"{nameof(SentenceWindowRetrievalOptions.Range)} must be less than or equal to 3.");
    }
}