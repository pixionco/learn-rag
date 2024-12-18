using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Entities.Basic;

public record BasicMetadata(Guid DocumentId, int Index, BasicEmbeddingOptions EmbeddingOptions)
    : IMetadata;