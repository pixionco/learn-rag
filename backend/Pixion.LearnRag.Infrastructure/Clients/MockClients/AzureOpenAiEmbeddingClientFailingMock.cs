﻿using Microsoft.Extensions.Options;
using Pixion.LearnRag.Infrastructure.Configs;
using Pixion.LearnRag.UseCases.Common.Clients;
using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.Infrastructure.Clients.MockClients;

public class AzureOpenAiEmbeddingClientFailingMock(IOptions<VectorDatabaseConfig> config) : IEmbeddingClient
{
    private readonly Random _random = new();

    public Task<Optional<ReadOnlyMemory<float>>> GenerateEmbeddingAsync(string text)
    {
        var isSuccess = Convert.ToBoolean(new Random().Next(0, 2));
        if (!isSuccess)
            return Task.FromResult(new Optional<ReadOnlyMemory<float>>(new Exception("Mock embedding failed!")));

        var embedding = GenerateRandomEmbedding();
        return Task.FromResult(new Optional<ReadOnlyMemory<float>>(embedding));
    }

    public Task<Optional<IList<ReadOnlyMemory<float>>>> GenerateEmbeddingsAsync(IList<string> text)
    {
        var isSuccess = Convert.ToBoolean(new Random().Next(0, 2));

        if (!isSuccess)
            return Task.FromResult(
                new Optional<IList<ReadOnlyMemory<float>>>(new Exception("Mock embedding failed!"))
            );

        var embeddings = text.Select(_ => GenerateRandomEmbedding()).ToList();
        return Task.FromResult(new Optional<IList<ReadOnlyMemory<float>>>(embeddings));
    }

    private ReadOnlyMemory<float> GenerateRandomEmbedding()
    {
        var embedding = new float[config.Value.VectorSize];
        for (var i = 0; i < embedding.Length; i++)
            embedding[i] = (float)_random.NextDouble();
        return new ReadOnlyMemory<float>(embedding);
    }
}