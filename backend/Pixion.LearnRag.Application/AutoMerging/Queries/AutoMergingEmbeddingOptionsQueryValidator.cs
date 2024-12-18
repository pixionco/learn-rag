using FluentValidation;

namespace Pixion.LearnRag.UseCases.AutoMerging.Queries;

public class AutoMergingEmbeddingOptionsQueryValidator : AbstractValidator<AutoMergingEmbeddingOptionsQuery>
{
    public AutoMergingEmbeddingOptionsQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage(
                $"{nameof(AutoMergingEmbeddingOptionsQuery.DocumentId)} can't be null, empty, or a whitespace."
            );
    }
}