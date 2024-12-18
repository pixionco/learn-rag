using FluentValidation;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public class AutoMergingSearchQueryValidator : AbstractValidator<AutoMergingSearchQuery>
{
    public AutoMergingSearchQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage($"{nameof(AutoMergingSearchQuery.Query)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new AutoMergingEmbeddingOptionsValidator());
        RuleFor(x => x.RetrievalOptions).SetValidator(new AutoMergingRetrievalOptionsValidator());
    }
}