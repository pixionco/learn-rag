using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class QuestionGenerationServiceMock : IQuestionGenerationService
{
    public Task<Optional<IEnumerable<string>>> GenerateQuestionsAsync(string text, int numberOfQuestion)
    {
        var questions = new List<string>();
        while (questions.Count < numberOfQuestion)
            questions.Add("What is the meaning of life?");

        return Task.FromResult(new Optional<IEnumerable<string>>(questions));
    }
}