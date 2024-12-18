using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Basic.Commands;
using Pixion.LearnRag.UseCases.Basic.Queries;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.API.Endpoints;

public class BasicStrategy : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        var group = app.MapGroup("basic", "Basic Strategy")
            .MapGet(GetBasicEmbeddingOptions, "embedding-options")
            .MapGet(BasicSearch, "search")
            .MapGet(BasicPreview, "preview");

        if (!endpointsConfig.DisableEmbedEndpoints)
            group.MapPost(BasicEmbed, "embed");
    }

    public Task<IEnumerable<BasicEmbeddingOptions>> GetBasicEmbeddingOptions(
        ISender sender,
        [AsParameters] BasicEmbeddingOptionsQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<IEnumerable<SearchResult<BasicMetadata>>> BasicSearch(
        ISender sender,
        string query,
        [AsParameters] BasicEmbeddingOptions embeddingOptions,
        [AsParameters] BasicRetrievalOptions retrievalOptions
    )
    {
        return sender.Send(new BasicSearchQuery(query, embeddingOptions, retrievalOptions));
    }

    public Task<IEnumerable<EmbeddingRecord<BasicMetadata>>> BasicPreview(
        ISender sender,
        Guid documentId,
        [AsParameters] BasicEmbeddingOptions embeddingOptions,
        int limit
    )
    {
        return sender.Send(new BasicPreviewQuery(documentId, embeddingOptions, limit));
    }

    public Task BasicEmbed(
        ISender sender,
        BasicEmbedCommand command
    )
    {
        return sender.Send(command);
    }
}