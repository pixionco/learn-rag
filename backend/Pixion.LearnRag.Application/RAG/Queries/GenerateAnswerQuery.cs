using MediatR;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.RAG.Queries;

public record GenerateAnswerQuery(string Question, string Context) : IRequest<string>;

public class GenerateAnswerHandler(IAnswerGenerationService answerGenerationService)
    : IRequestHandler<GenerateAnswerQuery, string>
{
    public async Task<string> Handle(GenerateAnswerQuery request, CancellationToken cancellationToken)
    {
        var answer = await answerGenerationService.GenerateAnswerAsync(request.Question, request.Context);
        if (!answer.Ok)
            throw answer.Exception;

        return answer.Value;
    }
}