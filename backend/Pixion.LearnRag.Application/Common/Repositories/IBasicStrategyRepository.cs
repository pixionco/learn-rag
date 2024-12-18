using Pixion.LearnRag.Core.Entities.Basic;
using Pixion.LearnRag.UseCases.Common.Services;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface IBasicStrategyRepository : IStrategyRepository<BasicMetadata, BasicEmbeddingOptions>;