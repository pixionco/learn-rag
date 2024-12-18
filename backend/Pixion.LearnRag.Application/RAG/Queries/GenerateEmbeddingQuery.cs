using MediatR;
using Pixion.LearnRag.UseCases.Common.Clients;

namespace Pixion.LearnRag.UseCases.RAG.Queries;

public record GenerateEmbeddingQuery(string Text) : IRequest<ReadOnlyMemory<float>>;

public class EmbedHandler(IEmbeddingClient embeddingClient)
    : IRequestHandler<GenerateEmbeddingQuery, ReadOnlyMemory<float>>
{
    public async Task<ReadOnlyMemory<float>> Handle(GenerateEmbeddingQuery request, CancellationToken cancellationToken)
    {
        var answer = await embeddingClient.GenerateEmbeddingAsync(request.Text);
        if (!answer.Ok)
            throw answer.Exception;

        return answer.Value;
    }
}