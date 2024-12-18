using FluentValidation;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public class HypotheticalQuestionSearchQueryValidator : AbstractValidator<HypotheticalQuestionSearchQuery>
{
    public HypotheticalQuestionSearchQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage($"{nameof(HypotheticalQuestionSearchQuery.Query)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new HypotheticalQuestionEmbeddingOptionsValidator());
        RuleFor(x => x.RetrievalOptions).SetValidator(new HypotheticalQuestionRetrievalOptionsValidator());
    }
}