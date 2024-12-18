using Pixion.LearnRag.Core.Exceptions;

namespace Pixion.LearnRag.UseCases.Common.Exceptions;

public class LRAlreadyEmbededException(
    string strategy,
    Guid documentId,
    object embeddingOptions
)
    : LearnRAGException(
        "Document already embedded",
        $"The strategy '{strategy}' has already embedded document with ID '{documentId}' with embedding options '{embeddingOptions}'."
    )
{
}