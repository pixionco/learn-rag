using FluentValidation;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public class SentenceWindowSearchQueryValidator : AbstractValidator<SentenceWindowSearchQuery>
{
    public SentenceWindowSearchQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage($"{nameof(SentenceWindowSearchQuery.Query)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new SentenceWindowEmbeddingOptionsValidator());
        RuleFor(x => x.RetrievalOptions).SetValidator(new SentenceWindowRetrievalOptionsValidator());
    }
}