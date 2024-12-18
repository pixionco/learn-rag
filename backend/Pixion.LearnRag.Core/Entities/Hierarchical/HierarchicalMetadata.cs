using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.Hierarchical;

public record HierarchicalMetadata(
    Guid DocumentId,
    int Index,
    int? ParentIndex,
    string? Summary,
    HierarchicalEmbeddingOptions EmbeddingOptions
) : IMetadata;