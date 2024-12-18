using MediatR;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.UseCases.Documents;

public record GetDocumentsQuery : IRequest<IEnumerable<Document>>;

public class GetDocumentsHandler(
    IDocumentRepository documentRepository
) : IRequestHandler<GetDocumentsQuery, IEnumerable<Document>>
{
    public async Task<IEnumerable<Document>> Handle(
        GetDocumentsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await documentRepository.GetDocumentsAsync(cancellationToken);
    }
}