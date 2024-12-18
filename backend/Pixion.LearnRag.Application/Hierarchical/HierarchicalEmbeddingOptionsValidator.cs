using FluentValidation;
using Pixion.LearnRag.Core.Entities.Hierarchical;

namespace Pixion.LearnRag.UseCases.Hierarchical;

public class HierarchicalEmbeddingOptionsValidator : AbstractValidator<HierarchicalEmbeddingOptions>
{
    public HierarchicalEmbeddingOptionsValidator()
    {
        RuleFor(x => x.ChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.");

        RuleFor(x => x.ChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50.");

        RuleFor(x => x.ChildChunkSize)
            .GreaterThan((ushort)0)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkSize)} must be greater than 0.")
            .LessThanOrEqualTo((ushort)2048)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkSize)} must be less than or equal to 2048.")
            .Must((options, childChunkSize) => childChunkSize < options.ChunkSize)
            .WithMessage(
                $"{nameof(HierarchicalEmbeddingOptions.ChunkSize)} must be less than {nameof(HierarchicalEmbeddingOptions.ChunkSize)}."
            );

        RuleFor(x => x.ChildChunkOverlap)
            .GreaterThanOrEqualTo((ushort)0)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkOverlap)} must be greater than or equal to 0")
            .LessThanOrEqualTo((ushort)50)
            .WithMessage($"{nameof(HierarchicalEmbeddingOptions.ChunkOverlap)} must be less than or equal to 50.");
    }
}