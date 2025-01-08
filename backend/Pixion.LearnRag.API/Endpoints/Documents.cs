using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.UseCases.Documents;

namespace Pixion.LearnRag.API.Endpoints;

public class Documents : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        app.MapGroup("documents", "Documents")
            .MapGet(GetDocuments);
    }

    public Task<IEnumerable<Document>> GetDocuments(ISender sender)
    {
        return sender.Send(new GetDocumentsQuery());
    }
}