using FluentValidation;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion;

public class HypotheticalQuestionRetrievalOptionsValidator : AbstractValidator<HypotheticalQuestionRetrievalOptions>
{
    public HypotheticalQuestionRetrievalOptionsValidator()
    {
        RuleFor(x => x.Limit)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HypotheticalQuestionRetrievalOptions.Limit)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)6)
            .WithMessage($"{nameof(HypotheticalQuestionRetrievalOptions.Limit)} must be less than or equal to 6.");
    }
}