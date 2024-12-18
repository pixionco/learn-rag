using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Services.MockServices;

public class QuestionGenerationServiceFailingMock : IQuestionGenerationService
{
    private readonly Random _random = new();

    public Task<Optional<IEnumerable<string>>> GenerateQuestionsAsync(string text, int numberOfQuestion)
    {
        var isSuccess = Convert.ToBoolean(_random.Next(0, 2));

        if (!isSuccess)
            return Task.FromResult(new Optional<IEnumerable<string>>(new Exception("Question generation failed!")));

        var questions = new List<string>();
        while (questions.Count < numberOfQuestion)
            questions.Add("What is the meaning of life?");

        return Task.FromResult(new Optional<IEnumerable<string>>(questions));
    }
}