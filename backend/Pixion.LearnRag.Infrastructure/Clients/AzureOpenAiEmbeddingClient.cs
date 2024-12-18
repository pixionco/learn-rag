using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Embeddings;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.Infrastructure.Utils;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.Infrastructure.Clients;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001

public class AzureOpenAiEmbeddingClient(IOptions<AzureOpenAiEmbeddingConfig> config) : IEmbeddingClient
{
    private readonly AzureOpenAITextEmbeddingGenerationService _embeddingService = new(
        config.Value.DeploymentName,
        config.Value.Endpoint,
        config.Value.ApiKey
    );

    public async Task<Optional<IList<ReadOnlyMemory<float>>>> GenerateEmbeddingsAsync(IList<string> texts)
    {
        return await PolicyHandler.ExecuteWithRetryAsync(async () => await GenerateEmbeddingsHelperAsync(texts));
    }

    public async Task<Optional<ReadOnlyMemory<float>>> GenerateEmbeddingAsync(string text)
    {
        return await PolicyHandler.ExecuteWithRetryAsync(async () => await GenerateEmbeddingHelperAsync(text));
    }

    private async Task<Optional<IList<ReadOnlyMemory<float>>>> GenerateEmbeddingsHelperAsync(IList<string> texts)
    {
        try
        {
            var embeddings = await _embeddingService.GenerateEmbeddingsAsync(texts);

            return new Optional<IList<ReadOnlyMemory<float>>>(embeddings);
        }
        catch (Exception ex)
        {
            return new Optional<IList<ReadOnlyMemory<float>>>(ex);
        }
    }

    private async Task<Optional<ReadOnlyMemory<float>>> GenerateEmbeddingHelperAsync(string text)
    {
        try
        {
            var embedding = await _embeddingService.GenerateEmbeddingAsync(text);

            return new Optional<ReadOnlyMemory<float>>(embedding);
        }
        catch (Exception ex)
        {
            return new Optional<ReadOnlyMemory<float>>(ex);
        }
    }
}