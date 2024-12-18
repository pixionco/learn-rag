using FluentValidation;

namespace Pixion.LearnRag.UseCases.SentenceWindow.Queries;

public class SentenceWindowEmbeddingOptionsQueryValidator : AbstractValidator<SentenceWindowEmbeddingOptionsQuery>
{
    public SentenceWindowEmbeddingOptionsQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(SentenceWindowEmbeddingOptionsQuery.DocumentId)} can't be null, empty, or a whitespace."
            );
    }
}