using System.Net;
using Microsoft.SemanticKernel;
using Pixion.LearnRag.UseCases.Common.Models;
using Polly;

namespace Pixion.LearnRag.Infrastructure.Utils;

public static class PolicyHandler
{
    public static async Task<Optional<T>> ExecuteWithRetryAsync<T>(
        Func<Task<Optional<T>>> operation
    )
    {
        return await Policy
            .HandleResult<Optional<T>>(
                result => result.Exception is HttpOperationException
                {
                    StatusCode: HttpStatusCode.TooManyRequests
                }
            )
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(operation);
    }
}