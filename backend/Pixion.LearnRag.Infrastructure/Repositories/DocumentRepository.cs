using Microsoft.Extensions.Options;
using Npgsql;
using Pixion.LearnRag.Core.Entities;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Repositories;

namespace Pixion.LearnRag.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly NpgsqlDataSource _npgsql;

    public DocumentRepository(IOptions<DocumentDatabaseConfig> config)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(config.Value.DatabaseConnection);
        dataSourceBuilder.UseVector();
        _npgsql = dataSourceBuilder.Build();
    }

    public async Task<Document?> GetDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        await using var connection = await _npgsql.OpenConnectionAsync(cancellationToken);

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = """
                          SELECT id, name, text
                          FROM documents
                          WHERE id = @documentId
                          """;
        cmd.Parameters.AddWithValue("documentId", documentId);

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        if (await reader.ReadAsync(cancellationToken)) return ReadDocument(reader);

        return null;
    }

    public async Task<IEnumerable<Document>> GetDocumentsAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await _npgsql.OpenConnectionAsync(cancellationToken);

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = """
                          SELECT id, name, text
                          FROM documents
                          """;

        var documents = new List<Document>();

        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken)) documents.Add(ReadDocument(reader));

        return documents;
    }

    private static Document ReadDocument(NpgsqlDataReader dataReader)
    {
        var id = dataReader.GetGuid(dataReader.GetOrdinal("id"));
        var name = dataReader.GetString(dataReader.GetOrdinal("name"));
        var text = dataReader.GetString(dataReader.GetOrdinal("text"));

        return new Document(id, name, text);
    }
}