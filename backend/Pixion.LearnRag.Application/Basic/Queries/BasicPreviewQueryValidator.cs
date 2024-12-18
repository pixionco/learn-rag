using FluentValidation;

namespace Pixion.LearnRag.UseCases.Basic.Queries;

public class BasicPreviewQueryValidator : AbstractValidator<BasicPreviewQuery>
{
    public BasicPreviewQueryValidator()
    {
        RuleFor(x => x.DocumentId)
            .NotEmpty()
            .WithMessage($"{nameof(BasicPreviewQuery.DocumentId)} can't be null, empty, or a whitespace.");

        RuleFor(x => x.EmbeddingOptions).SetValidator(new BasicEmbeddingOptionsValidator());

        RuleFor(x => x.Limit)
            .GreaterThan(0)
            .WithMessage($"{nameof(BasicPreviewQuery.Limit)} must be greater than 0")
            .LessThanOrEqualTo(10)
            .WithMessage($"{nameof(BasicPreviewQuery.Limit)} must be less than or equal to 10");
    }
}