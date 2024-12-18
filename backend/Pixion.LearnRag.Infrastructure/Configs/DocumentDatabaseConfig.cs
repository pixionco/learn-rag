namespace Pixion.LearnRag.Infrastructure.Configs;

public class DocumentDatabaseConfig
{
    public const string Key = "DocumentDatabase";

    public string DatabaseConnection { get; init; } = string.Empty;
}