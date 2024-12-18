using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.API.Infrastructure;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.SentenceWindow.Commands;
using Pixion.LearnRag.UseCases.SentenceWindow.Queries;

namespace Pixion.LearnRag.API.Endpoints;

public class SentenceWindowStrategy : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        var group = app.MapGroup("sentence-window", "Sentence Window Strategy")
            .MapGet(GetSentenceWindowEmbeddingOptions, "embedding-options")
            .MapGet(SentenceWindowSearch, "search")
            .MapGet(SentenceWindowPreview, "preview");

        if (!endpointsConfig.DisableEmbedEndpoints)
            group.MapPost(SentenceWindowEmbed, "embed");
    }

    public Task<IEnumerable<SentenceWindowEmbeddingOptions>> GetSentenceWindowEmbeddingOptions(
        ISender sender,
        [AsParameters] SentenceWindowEmbeddingOptionsQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<IEnumerable<SearchResult<SentenceWindowMetadata>>> SentenceWindowSearch(
        ISender sender,
        string query,
        [AsParameters] SentenceWindowEmbeddingOptions embeddingOptions,
        [AsParameters] SentenceWindowRetrievalOptions retrievalOptions
    )
    {
        return sender.Send(new SentenceWindowSearchQuery(query, embeddingOptions, retrievalOptions));
    }

    public Task<IEnumerable<EmbeddingRecord<SentenceWindowMetadata>>> SentenceWindowPreview(
        ISender sender,
        Guid documentId,
        [AsParameters] SentenceWindowEmbeddingOptions embeddingOptions,
        int limit
    )
    {
        return sender.Send(new SentenceWindowPreviewQuery(documentId, embeddingOptions, limit));
    }

    public Task SentenceWindowEmbed(
        ISender sender,
        SentenceWindowEmbedCommand command
    )
    {
        return sender.Send(command);
    }
}