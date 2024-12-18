using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IAnswerGenerationService
{
    public Task<Optional<string>> GenerateAnswerAsync(string question, string context);
}