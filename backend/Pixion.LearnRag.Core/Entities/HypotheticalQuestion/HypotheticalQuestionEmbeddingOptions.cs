using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.HypotheticalQuestion;

public readonly record struct HypotheticalQuestionEmbeddingOptions(
    ushort ChunkSize,
    ushort ChunkOverlap,
    ushort NumberOfQuestions
) : IEmbeddingOptions;