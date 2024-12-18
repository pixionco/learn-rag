using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class SummaryGenerationServiceFailingMock : ISummaryGenerationService
{
    private readonly Random _random = new();

    public Task<Optional<string>> GenerateSummaryAsync(string text)
    {
        var isSuccess = Convert.ToBoolean(_random.Next(0, 2));

        if (!isSuccess) return Task.FromResult(new Optional<string>(new Exception("Summary generation failed!")));

        return Task.FromResult(new Optional<string>(text));
    }
}