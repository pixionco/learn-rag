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

public class QuestionGenerationService(
    Kernel kernel,
    IPromptTemplateClient promptTemplateClient
) : IQuestionGenerationService
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<Optional<IEnumerable<string>>> GenerateQuestionsAsync(string text, int numberOfQuestions)
    {
        return await PolicyHandler.ExecuteWithRetryAsync(
            async () => await GenerateQuestionsHelperAsync(text, numberOfQuestions)
        );
    }

    private async Task<Optional<IEnumerable<string>>> GenerateQuestionsHelperAsync(string text, int numberOfQuestions)
    {
        var promptTemplate = promptTemplateClient.GetQuestionPromptTemplate();

        var generateQuestions =
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
            var res = await generateQuestions.InvokeAsync<QuestionGenerationResponse>(
                kernel,
                new KernelArguments
                {
                    { promptTemplate.NumberOfQuestionsKey, numberOfQuestions },
                    { promptTemplate.TextKey, text }
                }
            );

            return new Optional<IEnumerable<string>>(res!.Questions);
        }
        catch (InvalidCastException ex)
        {
            return new Optional<IEnumerable<string>>(ex);
        }
    }

    private class QuestionGenerationResponse
    {
        public List<string> Questions { get; } = [];
    }
}