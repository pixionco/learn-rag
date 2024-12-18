using Microsoft.Extensions.Options;
using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class BasicStrategyRepository(IOptions<VectorDatabaseConfig> config)
    : StrategyRepository<BasicMetadata, BasicEmbeddingOptions>(config, Strategy.Basic), IBasicStrategyRepository;