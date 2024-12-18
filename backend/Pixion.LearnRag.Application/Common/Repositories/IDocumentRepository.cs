using Pixion.LearnRag.Core.Entities;

namespace Pixion.LearnRag.UseCases.Common.Repositories;

public interface IDocumentRepository
{
    Task<Document?> GetDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Document>> GetDocumentsAsync(CancellationToken cancellationToken = default);
}