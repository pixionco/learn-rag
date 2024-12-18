namespace Pixion.LearnRag.Infrastructure.Configs;

public class AzureOpenAiEmbeddingConfig
{
    public const string Key = "SemanticKernel:AzureOpenAIEmbedding";

    public string ApiKey { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
}