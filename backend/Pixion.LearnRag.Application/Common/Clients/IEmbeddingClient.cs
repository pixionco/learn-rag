﻿using Pixion.LearnRag.UseCases.Common.Models;

namespace Pixion.LearnRag.UseCases.Common.Clients;

public interface IEmbeddingClient
{
    public Task<Optional<ReadOnlyMemory<float>>> GenerateEmbeddingAsync(string text);
    public Task<Optional<IList<ReadOnlyMemory<float>>>> GenerateEmbeddingsAsync(IList<string> text);
}