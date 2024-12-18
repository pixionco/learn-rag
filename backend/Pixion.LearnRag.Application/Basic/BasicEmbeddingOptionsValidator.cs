using FluentValidation;
using Pixion.LearnRag.Core.Entities.Basic;

namespace Pixion.LearnRag.UseCases.Basic;

public class BasicEmbeddingOptionsValidator : AbstractValidator<BasicEmbeddingOptions>
{
    public BasicEmbeddingOptionsValidator()
    {
        RuleFor(x => x.ChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(BasicEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(BasicEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.");

        RuleFor(x => x.ChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(BasicEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage($"{nameof(BasicEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50.");
    }
}