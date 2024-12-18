using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.SentenceWindow;

public readonly record struct SentenceWindowEmbeddingOptions(ushort ChunkSize) : IEmbeddingOptions;