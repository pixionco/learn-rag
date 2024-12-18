namespace Pixion.LearnRag.Core.Exceptions;

public class LRMetadataDeserializationException(
    Type metadataType,
    string metadataString,
    Exception? ex = default
)
    : LearnRAGException(
        nameof(LRMetadataDeserializationException),
        $"Failed to deserialize metadata '{metadataType}' from given metadata string: {metadataString}.",
        ex
    )
{
}