using DotNetCore.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace iiwi.NetLine.Extensions;

/// <summary>
/// Provides extension methods for converting business layer results to HTTP responses
/// </summary>
/// <remarks>
/// These extensions standardize the translation between application layer results
/// and API responses, ensuring consistent HTTP status codes and error handling.
/// </remarks>
public static class APIResultExtension
{
    /// <summary>
    /// Converts a typed result to an appropriate HTTP response
    /// </summary>
    /// <typeparam name="T">The type of the result value</typeparam>
    /// <param name="result">The result to convert</param>
    /// <returns>An IResult representing the HTTP response</returns>
    /// <remarks>
    /// Maps common HTTP status codes to appropriate response types:
    /// - OK (200) → Returns the value
    /// - Created (201) → Returns CreatedAtRoute response
    /// - Accepted (202) → Returns AcceptedAtRoute response
    /// - NoContent (204) → Empty response
    /// - BadRequest (400) → Validation problem response
    /// </remarks>
    public static IResult Response<T>(this Result<T> result)
    {
        if (result == null)
        {
            return TypedResults.Problem("Something went wrong");
        }
        var status = result.Status;

        return status switch
        {
            HttpStatusCode.OK => TypedResults.Ok(result.Value),
            HttpStatusCode.Created => TypedResults.CreatedAtRoute(result.Value),
            HttpStatusCode.Accepted => TypedResults.AcceptedAtRoute(result.Value),
            HttpStatusCode.NoContent => TypedResults.NoContent(),
            HttpStatusCode.BadRequest => CreateValidationProblem("BadRequest", result.Message),
            _ => TypedResults.Empty
        };
    }

    /// <summary>
    /// Converts a task-wrapped typed result to an HTTP response
    /// </summary>
    /// <typeparam name="T">The type of the result value</typeparam>
    /// <param name="result">The task-wrapped result</param>
    /// <returns>An IResult representing the HTTP response</returns>
    /// <remarks>
    /// Synchronously waits for the task and delegates to the non-task version
    /// </remarks>
    public static IResult Response<T>(this Task<Result<T>> result)
    {
        return result?.Result.Response();
    }

    /// <summary>
    /// Converts a non-typed result to an HTTP response
    /// </summary>
    /// <param name="result">The result to convert</param>
    /// <returns>An OK (200) response</returns>
    public static IResult Response(this Result result)
    {
        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Converts a task-wrapped non-typed result to an HTTP response
    /// </summary>
    /// <param name="result">The task-wrapped result</param>
    /// <returns>An OK (200) response</returns>
    public static IResult Response(this Task<Result> result)
    {
        return result?.Result.Response();
    }

    /// <summary>
    /// Wraps a direct value in an OK (200) response
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="result">The value to return</param>
    /// <returns>An OK (200) response containing the value</returns>
    public static IResult Response<T>(this T result)
    {
        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Wraps a task-wrapped value in an OK (200) response
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="result">The task-wrapped value</param>
    /// <returns>An OK (200) response containing the value</returns>
    public static IResult Response<T>(this Task<T> result)
    {
        return result?.Result.Response();
    }

    /// <summary>
    /// Asynchronously converts a task-wrapped typed result to an HTTP response
    /// </summary>
    /// <typeparam name="T">The type of the result value</typeparam>
    /// <param name="resultTask">The asynchronous result task</param>
    /// <returns>A task that completes with the HTTP response</returns>
    /// <remarks>
    /// Provides better async support than the synchronous version by:
    /// - Properly awaiting the task
    /// - Using ConfigureAwait(false)
    /// - Handling null results
    /// </remarks>
    public static async Task<IResult> ResponseAsync<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        if (result == null) return TypedResults.NotFound();
        return result.Status == HttpStatusCode.OK
            ? TypedResults.Ok(result.Value)
            : TypedResults.StatusCode((int)result.Status);
    }

    /// <summary>
    /// Creates a validation problem response
    /// </summary>
    /// <param name="errorCode">The error code</param>
    /// <param name="errorDescription">The error description</param>
    /// <returns>A validation problem response</returns>
    private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
       TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
       });
}