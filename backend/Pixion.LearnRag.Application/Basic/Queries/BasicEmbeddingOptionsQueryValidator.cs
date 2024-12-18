using FluentValidation;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public class BasicEmbeddingOptionsQueryValidator : AbstractValidator<BasicEmbeddingOptionsQuery>
{
    public BasicEmbeddingOptionsQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(BasicEmbeddingOptionsQuery.DocumentId)} can't be null, empty, or a whitespace.");
    }
}