using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Services;

public interface IQuestionGenerationService
{
    public Task<Optional<IEnumerable<string>>> GenerateQuestionsAsync(
        string text,
        int numberOfQuestion
    );
}