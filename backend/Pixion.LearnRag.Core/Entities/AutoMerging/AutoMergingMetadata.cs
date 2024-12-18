using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.AutoMerging;

public record AutoMergingMetadata(
    Guid DocumentId,
    int Index,
    int? ParentIndex,
    AutoMergingEmbeddingOptions EmbeddingOptions
) : IMetadata;