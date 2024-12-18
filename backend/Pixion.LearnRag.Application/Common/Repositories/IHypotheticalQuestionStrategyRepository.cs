using Pixion.LearnRag.Core.Entities.HypotheticalQuestion;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface
    IHypotheticalQuestionStrategyRepository : IStrategyRepository<HypotheticalQuestionMetadata,
    HypotheticalQuestionEmbeddingOptions>;