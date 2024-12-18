namespace Pixion.LearnRag.Core.Entities.AutoMerging;

public readonly record struct AutoMergingRetrievalOptions(
    ushort Limit,
    double ChildParentPrevalenceFactor
);