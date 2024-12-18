namespace Pixion.LearnRag.Infrastructure.Seeders;

public interface ISeeder
{
    public Task SeedAsync(CancellationToken cancellationToken = default);
}