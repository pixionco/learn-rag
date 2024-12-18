namespace Pixion.LearnRag.Infrastructure.Configs;

public class MockConfig
{
    public const string Key = "Mock";

    public bool MockAiModels { get; init; }
    public bool UseFailingMocks { get; init; }
}