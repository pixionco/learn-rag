using MediatR;
using Pixion.LearnRag.Core.Entities.PromptTemplates;
using Pixion.LearnRag.UseCases.Common.Clients;

namespace Pixion.LearnRag.UseCases.RAG.Queries;

public record AnswerTemplateQuery : IRequest<AnswerPromptTemplate>;

public class GetAnswerTemplateHandler(
    IPromptTemplateClient templateClient
) : IRequestHandler<AnswerTemplateQuery, AnswerPromptTemplate>
{
    public Task<AnswerPromptTemplate> Handle(
        AnswerTemplateQuery request,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(templateClient.GetAnswerPromptTemplate());
    }
}