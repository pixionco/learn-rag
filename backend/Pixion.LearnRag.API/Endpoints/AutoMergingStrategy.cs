using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.UseCases.AutoMerging.Commands;
using Pixion.LearnRag.UseCases.AutoMerging.Queries;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.API.Endpoints;

public class AutoMergingStrategy : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        var group = app.MapGroup("auto-merging", "Auto-Merging Strategy")
            .MapGet(GetAutoMergingEmbeddingOptions, "embedding-options")
            .MapGet(AutoMergingSearch, "search")
            .MapGet(AutoMergingPreview, "preview");

        if (!endpointsConfig.DisableEmbedEndpoints)
            group.MapPost(AutoMergingEmbed, "embed");
    }

    public Task<IEnumerable<AutoMergingEmbeddingOptions>> GetAutoMergingEmbeddingOptions(
        ISender sender,
        [AsParameters] AutoMergingEmbeddingOptionsQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<IEnumerable<SearchResult<AutoMergingMetadata>>> AutoMergingSearch(
        ISender sender,
        string query,
        [AsParameters] AutoMergingEmbeddingOptions embeddingOptions,
        [AsParameters] AutoMergingRetrievalOptions retrievalOptions
    )
    {
        return sender.Send(
            new AutoMergingSearchQuery(
                query,
                embeddingOptions,
                retrievalOptions
            )
        );
    }

    public Task<IEnumerable<EmbeddingRecord<AutoMergingMetadata>>> AutoMergingPreview(
        ISender sender,
        Guid documentId,
        [AsParameters] AutoMergingEmbeddingOptions embeddingOptions,
        int limit
    )
    {
        return sender.Send(
            new AutoMergingPreviewQuery(
                documentId,
                embeddingOptions,
                limit
            )
        );
    }

    public Task AutoMergingEmbed(
        ISender sender,
        AutoMergingEmbedCommand command
    )
    {
        return sender.Send(command);
    }
}