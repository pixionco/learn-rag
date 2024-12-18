using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.HypotheticalQuestion;

public record HypotheticalQuestionMetadata(
    Guid DocumentId,
    int Index,
    HypotheticalQuestionEmbeddingOptions EmbeddingOptions,
    IEnumerable<string> Questions
) : IMetadata;