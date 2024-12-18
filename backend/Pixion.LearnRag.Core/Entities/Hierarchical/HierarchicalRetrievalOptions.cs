namespace Pixion.LearnRag.Core.Entities.Hierarchical;

public readonly record struct HierarchicalRetrievalOptions(
    ushort Limit,
    ushort ChildLimit
);