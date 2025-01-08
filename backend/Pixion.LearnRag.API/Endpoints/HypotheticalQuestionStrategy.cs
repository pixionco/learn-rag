using MediatR;
using Pixion.LearnRag.API.Configs;
using Pixion.LearnRag.API.Extensions;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Models;
using Pixion.LearnRag.UseCases.HypotheticalQuestion.Commands;
using Pixion.LearnRag.UseCases.HypotheticalQuestion.Queries;

namespace Pixion.LearnRag.API.Endpoints;

public class HypotheticalQuestionStrategy : EndpointGroupBase
{
    public override void Map(WebApplication app, EndpointsConfig endpointsConfig)
    {
        var group = app.MapGroup("hypothetical-question", "Hypothetical Question Strategy")
            .MapGet(GetHypotheticalQuestionEmbeddingOptions, "embedding-options")
            .MapGet(HypotheticalQuestionSearch, "search")
            .MapGet(HypotheticalQuestionPreview, "preview");

        if (!endpointsConfig.DisableEmbedEndpoints)
            group.MapPost(HypotheticalQuestionEmbed, "embed");
    }

    public Task<IEnumerable<HypotheticalQuestionEmbeddingOptions>> GetHypotheticalQuestionEmbeddingOptions(
        ISender sender,
        [AsParameters] HypotheticalQuestionEmbeddingOptionsQuery query
    )
    {
        return sender.Send(query);
    }

    public Task<IEnumerable<SearchResult<HypotheticalQuestionMetadata>>> HypotheticalQuestionSearch(
        ISender sender,
        string query,
        [AsParameters] HypotheticalQuestionEmbeddingOptions embeddingOptions,
        [AsParameters] HypotheticalQuestionRetrievalOptions retrievalOptions
    )
    {
        return sender.Send(new HypotheticalQuestionSearchQuery(query, embeddingOptions, retrievalOptions));
    }

    public Task<IEnumerable<EmbeddingRecord<HypotheticalQuestionMetadata>>> HypotheticalQuestionPreview(
        ISender sender,
        Guid documentId,
        [AsParameters] HypotheticalQuestionEmbeddingOptions embeddingOptions,
        int limit
    )
    {
        return sender.Send(new HypotheticalQuestionPreviewQuery(documentId, embeddingOptions, limit));
    }

    public Task HypotheticalQuestionEmbed(
        ISender sender,
        HypotheticalQuestionEmbedCommand command
    )
    {
        return sender.Send(command);
    }
}