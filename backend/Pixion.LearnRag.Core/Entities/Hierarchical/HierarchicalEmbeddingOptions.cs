using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.Hierarchical;

public readonly record struct HierarchicalEmbeddingOptions(
    ushort ChunkSize,
    ushort ChunkOverlap,
    ushort ChildChunkSize,
    ushort ChildChunkOverlap
) : IEmbeddingOptions;