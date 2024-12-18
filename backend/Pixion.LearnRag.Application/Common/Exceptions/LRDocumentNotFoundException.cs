using Pixion.LearnRag.Core.Exceptions;

namespace Pixion.LearnRag.UseCases.Common.Exceptions;

public class LRDocumentNotFoundException(
    Guid documentId
)
    : LearnRAGException(
        "Document not found",
        $"The document with ID '{documentId}' was not found."
    )
{
}