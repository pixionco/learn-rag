using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Pixion.LearnRag.Infrastructure.Utils;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services;

#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0013

public class AnswerGenerationService(
    Kernel kernel,
    IPromptTemplateClient promptTemplateClient
) : IAnswerGenerationService
{
    public async Task<Optional<string>> GenerateAnswerAsync(string question, string context)
    {
        return await PolicyHandler.ExecuteWithRetryAsync(
            async () => await GenerateAnswerHelperAsync(question, context)
        );
    }

    private async Task<Optional<string>> GenerateAnswerHelperAsync(string question, string context)
    {
        var promptTemplate = promptTemplateClient.GetAnswerPromptTemplate();

        var answerGenerationFunction =
            kernel.CreateFunctionFromPrompt(
                promptTemplate.TemplateString,
                new OpenAIPromptExecutionSettings
                {
                    Temperature = 0.1
                }
            );

        try
        {
            var res = await answerGenerationFunction.InvokeAsync<string>(
                kernel,
                new KernelArguments
                {
                    { promptTemplate.Question, question },
                    { promptTemplate.Context, context }
                }
            );

            return new Optional<string>(res);
        }
        catch (Exception ex)
        {
            return new Optional<string>(ex);
        }
    }
}