using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.Basic;

public readonly record struct BasicEmbeddingOptions(ushort ChunkSize, ushort ChunkOverlap) : IEmbeddingOptions;