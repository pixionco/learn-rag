using FluentValidation;
using Pixion.LearnRag.Core.Entities.SentenceWindow;

namespace Pixion.LearnRag.UseCases.SentenceWindow;

public class SentenceWindowEmbeddingOptionsValidator : AbstractValidator<SentenceWindowEmbeddingOptions>
{
    public SentenceWindowEmbeddingOptionsValidator()
    {
        RuleFor(x => x.ChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(SentenceWindowEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(SentenceWindowEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.");
    }
}