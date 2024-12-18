using FluentValidation;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

public class
    HypotheticalQuestionEmbeddingOptionsQueryValidator : AbstractValidator<HypotheticalQuestionEmbeddingOptionsQuery>
{
    public HypotheticalQuestionEmbeddingOptionsQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbeddingOptionsQuery.DocumentId)} can't be null, empty, or a whitespace."
            );
    }
}