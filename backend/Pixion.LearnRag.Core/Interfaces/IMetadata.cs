namespace Pixion.LearnRag.Core.Interfaces;

public interface IMetadata
{
    Guid DocumentId { get; init; }
    int Index { get; init; }
}