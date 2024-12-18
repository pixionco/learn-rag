using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface ISummaryGenerationService
{
    public Task<Optional<string>> GenerateSummaryAsync(string text);
}