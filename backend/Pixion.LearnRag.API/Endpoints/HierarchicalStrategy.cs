using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.Hierarchical.Commands;
using Pixion.LearnRag.UseCases.Hierarchical.Queries;

namespace Pixion.LearnRag.API.Endpoints;

public class HierarchicalStrategy : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        var group = app.MapGroup("hierarchical", "Hierarchical Strategy")
            .MapGet(GetHierarchicalEmbeddingOptions, "embedding-options")
            .MapGet(HierarchicalSearch, "search")
            .MapGet(HierarchicalPreview, "preview");

        if (!endpointsConfig.DisableEmbedEndpoints)
            group.MapPost(HierarchicalEmbed, "embed");
    }

    public Task<IEnumerable<HierarchicalEmbeddingOptions>> GetHierarchicalEmbeddingOptions(
        ISender sender,
        [AsParameters] HierarchicalEmbeddingOptionsQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<IEnumerable<SearchResult<HierarchicalMetadata>>> HierarchicalSearch(
        ISender sender,
        string query,
        [AsParameters] HierarchicalEmbeddingOptions embeddingOptions,
        [AsParameters] HierarchicalRetrievalOptions retrievalOptions
    )
    {
        return sender.Send(new HierarchicalSearchQuery(query, embeddingOptions, retrievalOptions));
    }

    public Task<IEnumerable<EmbeddingRecord<HierarchicalMetadata>>> HierarchicalPreview(
        ISender sender,
        Guid documentId,
        [AsParameters] HierarchicalEmbeddingOptions embeddingOptions,
        int limit
    )
    {
        return sender.Send(new HierarchicalPreviewQuery(documentId, embeddingOptions, limit));
    }

    public Task HierarchicalEmbed(
        ISender sender,
        HierarchicalEmbedCommand command
    )
    {
        return sender.Send(command);
    }
}