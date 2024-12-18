using FluentValidation;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public class BasicSearchQueryValidator : AbstractValidator<BasicSearchQuery>
{
    public BasicSearchQueryValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage($"{nameof(BasicSearchQuery.Query)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new BasicEmbeddingOptionsValidator());
        RuleFor(x => x.RetrievalOptions).SetValidator(new BasicRetrievalOptionsValidator());
    }
}