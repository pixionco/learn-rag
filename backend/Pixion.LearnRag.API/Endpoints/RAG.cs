using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.Core.Entities.PromptTemplates;
using Pixion.LearnRag.UseCases.RAG.Queries;

namespace Pixion.LearnRag.API.Endpoints;

public class RAG : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        app.MapGroup("rag", "RAG Endpoints")
            .MapPost(GenerateEmbedding, "embedding")
            .MapPost(GenerateAnswer, "generate-answer")
            .MapGet(GetAnswerTemplate, "answer-template");
    }

    public Task<ReadOnlyMemory<float>> GenerateEmbedding(
        ISender sender,
        GenerateEmbeddingQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<string> GenerateAnswer(ISender sender, GenerateAnswerQuery query)
    {
        return sender.Send(query);
    }

    public Task<AnswerPromptTemplate> GetAnswerTemplate(ISender sender)
    {
        return sender.Send(new AnswerTemplateQuery());
    }
}