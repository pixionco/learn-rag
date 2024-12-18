using Microsoft.Extensions.Options;
using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class HypotheticalQuestionStrategyRepository(IOptions<VectorDatabaseConfig> config)
    : StrategyRepository<HypotheticalQuestionMetadata, HypotheticalQuestionEmbeddingOptions>(
            config,
            Strategy.HypotheticalQuestion
        ),
        IHypotheticalQuestionStrategyRepository;