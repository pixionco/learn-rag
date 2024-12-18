using FluentValidation;

namespace Pixion.LearnRag.UseCases.RAG.Queries;

public class GenerateEmbeddingQueryValidator : AbstractValidator<GenerateEmbeddingQuery>
{
    public GenerateEmbeddingQueryValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage($"{nameof(GenerateEmbeddingQuery.Text)} can't be null, empty or a whitespace.");
    }
}