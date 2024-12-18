using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.AutoMerging;

public readonly record struct AutoMergingEmbeddingOptions(
    ushort ChunkSize,
    ushort ChunkOverlap,
    ushort ChildChunkSize,
    ushort ChildChunkOverlap
) : IEmbeddingOptions;