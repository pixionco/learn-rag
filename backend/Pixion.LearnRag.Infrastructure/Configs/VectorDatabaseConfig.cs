namespace Pixion.LearnRag.Infrastructure.Configs;

public class VectorDatabaseConfig
{
    public const string Key = "VectorDatabase";

    public string DatabaseConnection { get; init; } = string.Empty;
    public int VectorSize { get; init; }
    public string Schema { get; init; } = string.Empty;
}