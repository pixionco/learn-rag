using Pixion.LearnRag.Core.Interfaces;
using Pixion.LearnRag.Core.Utils;

namespace Pixion.LearnRag.Core.Entities;

public record EmbeddingRecord<T> where T : IMetadata
{
    public EmbeddingRecord(Guid id, string text, ReadOnlyMemory<float> embedding, string metadataString)
    {
        Id = id;
        Text = text;
        Embedding = embedding;
        Metadata = MetadataHelper.DeserializeMetadata<T>(metadataString);
    }

    public EmbeddingRecord(Guid id, string text, ReadOnlyMemory<float> embedding, T metadata)
    {
        Id = id;
        Text = text;
        Embedding = embedding;
        Metadata = metadata;
    }

    public Guid Id { init; get; }
    public string Text { init; get; }
    public ReadOnlyMemory<float> Embedding { init; get; }
    public T Metadata { init; get; }
}