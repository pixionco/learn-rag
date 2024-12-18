using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.SentenceWindow;

public record SentenceWindowMetadata(
    Guid DocumentId,
    int Index,
    SentenceWindowEmbeddingOptions EmbeddingOptions,
    string OriginalText
) : IMetadata;