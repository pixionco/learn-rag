using FluentValidation;

namespace Pixion.LearnRag.UseCases.RAG.Queries;

public class GenerateAnswerQueryValidator : AbstractValidator<GenerateAnswerQuery>
{
    public GenerateAnswerQueryValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty()
            .WithMessage($"{nameof(GenerateAnswerQuery.Question)} can't be null, empty or a whitespace.");

        RuleFor(x => x.Context)
            .NotEmpty()
            .WithMessage($"{nameof(GenerateAnswerQuery.Context)} can't be null, empty or a whitespace.");
    }
}