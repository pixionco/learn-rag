using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class AnswerGenerationServiceFailingMock : IAnswerGenerationService
{
    private readonly Random _random = new();

    public Task<Optional<string>> GenerateAnswerAsync(string question, string context)
    {
        var isSuccess = Convert.ToBoolean(_random.Next(0, 2));

        if (!isSuccess)
            return Task.FromResult(new Optional<string>(new Exception("Answer generation failed!")));

        return Task.FromResult(new Optional<string>("mock answer"));
    }
}