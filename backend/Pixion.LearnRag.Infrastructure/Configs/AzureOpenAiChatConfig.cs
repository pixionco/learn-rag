namespace Pixion.LearnRag.Infrastructure.Configs;

public class AzureOpenAiChatConfig
{
    public const string Key = "SemanticKernel:AzureOpenAIChat";

    public string ApiKey { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
}