using Pixion.LearnRag.Core.Interfaces;
using Pixion.LearnRag.Core.Utils;

namespace Pixion.LearnRag.UseCases.Common.Models;

public record SearchResult<T> where T : IMetadata
{
    public SearchResult(Guid id, string text, double? relevance, string metadataString)
    {
        Id = id;
        Text = text;
        Relevance = relevance;
        Metadata = MetadataHelper.DeserializeMetadata<T>(metadataString);
    }

    public Guid Id { init; get; }
    public string Text { init; get; }
    public double? Relevance { init; get; }
    public T Metadata { init; get; }
}