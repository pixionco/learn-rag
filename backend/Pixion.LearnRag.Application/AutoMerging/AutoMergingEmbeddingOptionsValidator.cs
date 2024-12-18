using FluentValidation;
using Pixion.LearnRag.Core.Entities.AutoMerging;

namespace Pixion.LearnRag.UseCases.AutoMerging;

public class AutoMergingEmbeddingOptionsValidator : AbstractValidator<AutoMergingEmbeddingOptions>
{
    public AutoMergingEmbeddingOptionsValidator()
    {
        RuleFor(x => x.ChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.");

        RuleFor(x => x.ChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50.");

        RuleFor(x => x.ChildChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.")
            .Must((options, childChunkSize) => childChunkSize < options.ChunkSize)
            .WithMessage(
                $"{nameof(AutoMergingEmbeddingOptions.ChunkSize)} must be less than {nameof(AutoMergingEmbeddingOptions.ChunkSize)}."
            );

        RuleFor(x => x.ChildChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage($"{nameof(AutoMergingEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50.");
    }
}