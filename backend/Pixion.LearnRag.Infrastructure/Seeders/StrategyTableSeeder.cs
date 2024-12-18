using System.Reflection;
using Microsoft.Extensions.Options;
using Npgsql;
using Pixion.LearnRag.Core.Enums;
using Pixion.LearnRag.Infrastructure.Configs;

namespace Pixion.LearnRag.Infrastructure.Seeders;

public class StrategyTableSeeder : ISeeder
{
    private readonly NpgsqlDataSource _npgsql;
    private readonly string _schema;
    private readonly int _vectorSize;

    public StrategyTableSeeder(IOptions<VectorDatabaseConfig> config)
    {
        _schema = config.Value.Schema;
        _vectorSize = config.Value.VectorSize;

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(config.Value.DatabaseConnection);
        dataSourceBuilder.UseVector();
        _npgsql = dataSourceBuilder.Build();
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var connection = await _npgsql.OpenConnectionAsync(cancellationToken);
        var strategyNames = typeof(Strategy)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(string))
            .Select(f => (string)f.GetValue(null)!)
            .ToList();

        await using (connection)
        {
            // Make sure extension exists before we create vector columns
            await using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE EXTENSION IF NOT EXISTS vector";
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await connection.ReloadTypesAsync();

            foreach (var strategyName in strategyNames)
            {
                await CreateStrategyTableAsync(connection, strategyName, cancellationToken);
                await CreateStrategyTableMetadataIndexAsync(connection, strategyName, cancellationToken);
            }
        }
    }

    private async Task CreateStrategyTableAsync(
        NpgsqlConnection connection,
        string strategyName,
        CancellationToken cancellationToken
    )
    {
        await using var cmd = connection.CreateCommand();

        cmd.CommandText = $"""
                           CREATE TABLE IF NOT EXISTS {_schema}.{strategyName} (
                               id UUID NOT NULL PRIMARY KEY,
                               text TEXT NOT NULL,
                               embedding vector({_vectorSize}) NOT NULL,
                               metadata JSONB
                           )
                           """;
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task CreateStrategyTableMetadataIndexAsync(
        NpgsqlConnection connection,
        string strategyName,
        CancellationToken cancellationToken
    )
    {
        await using var cmd = connection.CreateCommand();

        // NOTE:
        // to use the index on JSONB fields we need to restrict ourselves to JSONB type operators    
        // https://www.postgresql.org/docs/current/datatype-json.html#JSON-INDEXING
        cmd.CommandText = $"""
                           CREATE INDEX IF NOT EXISTS {strategyName}_metadata_index
                               ON {_schema}.{strategyName}
                               USING GIN (metadata)
                           """;
        await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
}