using System.Text.Json;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;
using Pixion.LearnRag.Infrastructure.Utils;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services;

#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0013

public class SummaryGenerationService(
    Kernel kernel,
    IPromptTemplateClient promptTemplateClient
) : ISummaryGenerationService
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<Optional<string>> GenerateSummaryAsync(string text)
    {
        return await PolicyHandler.ExecuteWithRetryAsync(async () => await GenerateSummaryHelperAsync(text));
    }

    private async Task<Optional<string>> GenerateSummaryHelperAsync(string text)
    {
        var promptTemplate = promptTemplateClient.GetSummaryPromptTemplate();

        var summarize =
            kernel.CreateFunctionFromPrompt(
                promptTemplate.TemplateString,
                new OpenAIPromptExecutionSettings
                {
                    Temperature = 0.1,
                    ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
                }
            );

        try
        {
            var res = await summarize.InvokeAsync<SummaryGenerationResponse>(
                kernel,
                new KernelArguments { { promptTemplate.TextKey, text } }
            );

            return new Optional<string>(res!.Summary);
        }
        catch (InvalidCastException ex)
        {
            return new Optional<string>(ex);
        }
    }

    private class SummaryGenerationResponse
    {
        public string Summary { get; } = string.Empty;
    }
}