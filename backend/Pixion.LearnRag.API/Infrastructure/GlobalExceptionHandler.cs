using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Pixion.LearnRag.UseCases.Common.Exceptions;

namespace Pixion.LearnRag.API.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(
            exception,
            "Exception occurred: {Message}",
            exception.Message
        );

        var problemDetails = exception switch
        {
            LRDocumentNotFoundException ragException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = ragException.Title,
                Detail = ragException.Message
            },
            LRAlreadyEmbededException ragException => new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = ragException.Title,
                Detail = ragException.Message
            },
            LREmbeddingCountMismatchException ragException => new ProblemDetails
            {
                Status = StatusCodes.Status502BadGateway,
                Title = ragException.Title,
                Detail = ragException.Message
            },
            ValidationException validationException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Data validation failed",
                Detail = validationException.Message,
                Extensions = new Dictionary<string, object?> { { "errors", validationException.Errors.ToArray() } }
            },
            HttpOperationException httpOperationException => new ProblemDetails
            {
                Status = (int)(httpOperationException.StatusCode ?? HttpStatusCode.InternalServerError),
                Title = "AI Server error",
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected server error",
                Detail = exception.Message
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}