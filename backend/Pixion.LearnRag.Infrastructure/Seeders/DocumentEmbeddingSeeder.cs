using Pixion.LearnRag.Core.Entities.AutoMerging;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.Core.Entities.Hierarchical;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.Core.Entities.SentenceWindow;
using Pixion.LearnRag.UseCases.Common.Repositories;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.Infrastructure.Seeders;

public class DocumentEmbeddingSeeder(
    IDocumentRepository documentRepository,
    IBasicStrategyService basicStrategyService,
    ISentenceWindowStrategyService sentenceWindowStrategyService,
    IAutoMergingStrategyService autoMergingStrategyService,
    IHierarchicalStrategyService hierarchicalStrategyService,
    IHypotheticalQuestionStrategyService hypotheticalQuestionStrategyService
)
    : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var documents = (await documentRepository.GetDocumentsAsync(cancellationToken)).ToList();

        foreach (var document in documents)
        {
            Console.WriteLine($"Seeding document {document.Name}");

            foreach (var embeddingOption in _basicEmbeddingOptions)
                if (!(await basicStrategyService.PreviewAsync(document.Id, embeddingOption, 1, cancellationToken))
                    .Any())
                {
                    Console.WriteLine($"Seeding basic table with embedding option {embeddingOption}");
                    await basicStrategyService.EmbedAsync(document.Id, embeddingOption);
                }

            foreach (var embeddingOption in _sentenceWindowEmbeddingOptions)
                if (!(await sentenceWindowStrategyService.PreviewAsync(
                        document.Id,
                        embeddingOption,
                        1,
                        cancellationToken
                    ))
                    .Any())
                {
                    Console.WriteLine($"Seeding sentence window table with embedding option {embeddingOption}");
                    await sentenceWindowStrategyService.EmbedAsync(document.Id, embeddingOption);
                }

            foreach (var embeddingOption in _autoMergingEmbeddingOptions)
                if (!(await autoMergingStrategyService.PreviewAsync(
                        document.Id,
                        embeddingOption,
                        1,
                        cancellationToken
                    ))
                    .Any())
                {
                    Console.WriteLine($"Seeding auto merging table with embedding option {embeddingOption}");
                    await autoMergingStrategyService.EmbedAsync(document.Id, embeddingOption);
                }

            foreach (var embeddingOption in _hierarchicalEmbeddingOptions)
                if (!(await hierarchicalStrategyService.PreviewAsync(
                        document.Id,
                        embeddingOption,
                        1,
                        cancellationToken
                    ))
                    .Any())
                {
                    Console.WriteLine($"Seeding hierarchical table with embedding option {embeddingOption}");
                    await hierarchicalStrategyService.EmbedAsync(document.Id, embeddingOption);
                }

            foreach (var embeddingOption in _hypotheticalQuestionEmbeddingOptions)
                if (!(await hypotheticalQuestionStrategyService.PreviewAsync(
                        document.Id,
                        embeddingOption,
                        1,
                        cancellationToken
                    ))
                    .Any())
                {
                    Console.WriteLine($"Seeding hypothetical question table with embedding option {embeddingOption}");
                    await hypotheticalQuestionStrategyService.EmbedAsync(document.Id, embeddingOption);
                }
        }
    }

    #region embedding-options

    private readonly List<BasicEmbeddingOptions> _basicEmbeddingOptions =
    [
        new(256, 20),
        new(256, 0),

        new(128, 20),
        new(128, 0),

        new(512, 20),
        new(512, 0)
    ];

    private readonly List<SentenceWindowEmbeddingOptions> _sentenceWindowEmbeddingOptions =
    [
        new(256),
        new(128),
        new(64)
    ];

    private readonly List<AutoMergingEmbeddingOptions> _autoMergingEmbeddingOptions =
    [
        new(1024, 20, 256, 20),
        new(1024, 0, 256, 0),

        new(1024, 20, 128, 20),
        new(1024, 0, 128, 0),

        new(2048, 20, 512, 20),
        new(2048, 0, 512, 0),

        new(2048, 20, 256, 20),
        new(2048, 0, 256, 0)
    ];

    private readonly List<HierarchicalEmbeddingOptions> _hierarchicalEmbeddingOptions =
    [
        new(1024, 20, 256, 20),
        new(1024, 0, 256, 0),

        new(1024, 20, 128, 20),
        new(1024, 0, 128, 0),

        new(2048, 20, 512, 20),
        new(2048, 0, 512, 0),

        new(2048, 20, 256, 20),
        new(2048, 0, 256, 0)
    ];

    private readonly List<HypotheticalQuestionEmbeddingOptions> _hypotheticalQuestionEmbeddingOptions =
    [
        new(256, 20, 1),
        new(256, 0, 1),

        new(128, 20, 1),
        new(128, 0, 1),

        new(256, 20, 3),
        new(256, 0, 3),

        new(128, 20, 3),
        new(128, 0, 3)
    ];

    #endregion
}