using Pixion.LearnRag.Core.Exceptions;

namespace Pixion.LearnRag.UseCases.Common.Exceptions;

public class LREmbeddingCountMismatchException(
    int chunkCount,
    int embeddingCount
)
    : LearnRAGException(
        "Embedding count mismatch",
        $"The number of generated embeddings ({embeddingCount}) doesn't match the number of input chunks ({chunkCount})."
    )
{
}