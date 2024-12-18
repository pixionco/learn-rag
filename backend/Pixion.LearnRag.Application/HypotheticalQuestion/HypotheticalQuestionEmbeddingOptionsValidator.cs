using FluentValidation;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;

namespace Pixion.LearnRag.UseCases.HypotheticalQuestion;

public class HypotheticalQuestionEmbeddingOptionsValidator : AbstractValidator<HypotheticalQuestionEmbeddingOptions>
{
    public HypotheticalQuestionEmbeddingOptionsValidator()
    {
        RuleFor(x => x.ChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HypotheticalQuestionEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbeddingOptions.ChunkSize)} must be less than or equal to 2048."
            );

        RuleFor(x => x.ChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0"
            )
            .LessThanOrEqualTo((ushort)50)
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50."
            );

        RuleFor(x => x.NumberOfQuestions)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HypotheticalQuestionEmbeddingOptions.NumberOfQuestions)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)5)
            .WithMessage(
                $"{nameof(HypotheticalQuestionEmbeddingOptions.NumberOfQuestions)} must be less than or equal to 5."
            );
    }
}