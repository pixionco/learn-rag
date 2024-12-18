using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class AnswerGenerationServiceMock : IAnswerGenerationService
{
    public Task<Optional<string>> GenerateAnswerAsync(string question, string context)
    {
        return Task.FromResult(new Optional<string>("mock answer"));
    }
}