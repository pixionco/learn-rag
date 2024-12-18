using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class SummaryGenerationServiceMock : ISummaryGenerationService
{
    public Task<Optional<string>> GenerateSummaryAsync(string text)
    {
        return Task.FromResult(new Optional<string>(text));
    }
}