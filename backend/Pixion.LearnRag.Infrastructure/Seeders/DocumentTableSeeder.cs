using Microsoft.Extensions.Options;
using Npgsql;
using Pixion.LearnRag.Infrastructure.Configs;

namespace Pixion.LearnRag.Infrastructure.Seeders;

public class DocumentTableSeeder : ISeeder
{
    private readonly string _assetsFolder;
    private readonly NpgsqlDataSource _npgsql;
    private readonly string _schema;

    public DocumentTableSeeder(IOptions<VectorDatabaseConfig> config)
    {
        _schema = config.Value.Schema;
        _assetsFolder = Path.Combine(
            new DirectoryInfo(Directory.GetCurrentDirectory()).Parent!.FullName, // backend folder
            "Pixion.LearnRag.Infrastructure",
            "Assets"
        );

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(config.Value.DatabaseConnection);
        dataSourceBuilder.UseVector();
        _npgsql = dataSourceBuilder.Build();
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await _npgsql.OpenConnectionAsync(cancellationToken);

        await CreateDocumentsTableAsync(connection, cancellationToken);

        {
            await using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = $"""
                                    SELECT COUNT(*)
                                    FROM {_schema}.documents
                                    """;

            var result = await checkCmd.ExecuteScalarAsync(cancellationToken);
            var rowCount = Convert.ToInt32(result);

            if (rowCount == 0)
            {
                var files = Directory.GetFiles(_assetsFolder, "*.txt");

                foreach (var filePath in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var fileContents = await File.ReadAllTextAsync(filePath, cancellationToken);

                    await InsertDocumentAsync(connection, fileName, fileContents, cancellationToken);
                }
            }
        }
    }

    private async Task InsertDocumentAsync(
        NpgsqlConnection connection,
        string name,
        string text,
        CancellationToken cancellationToken
    )
    {
        await using var seedCmd = connection.CreateCommand();
        seedCmd.CommandText = $"""
                               INSERT INTO {_schema}.documents (id, name, text) VALUES
                                   (gen_random_uuid(), @name, @text)
                               """;

        seedCmd.Parameters.AddWithValue("name", name);
        seedCmd.Parameters.AddWithValue("text", text);

        await seedCmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task CreateDocumentsTableAsync(
        NpgsqlConnection connection,
        CancellationToken cancellationToken
    )
    {
        await using var cmd = connection.CreateCommand();

        cmd.CommandText = """
                          CREATE TABLE IF NOT EXISTS documents (
                              id UUID NOT NULL PRIMARY KEY,
                              name VARCHAR(255) NOT NULL,
                              text TEXT NOT NULL
                          )
                          """;
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}