using System.Text.Json;
using Pixion.LearnRag.Core.Exceptions;
using Pixion.LearnRag.Core.Interfaces;

namespace Pixion.LearnRag.Core.Utils;

public static class MetadataHelper
{
    public static T DeserializeMetadata<T>(string metadataString) where T : IMetadata
    {
        try
        {
            var metadataObject = JsonSerializer.Deserialize<T>(metadataString);
            if (metadataObject is null) throw new LRMetadataDeserializationException(typeof(T), metadataString);

            return metadataObject;
        }
        catch (Exception ex)
        {
            throw new LRMetadataDeserializationException(typeof(T), metadataString, ex);
        }
    }
}