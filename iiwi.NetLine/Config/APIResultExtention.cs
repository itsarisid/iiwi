using DotNetCore.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;
using System.Reflection.Metadata;

namespace iiwi.NetLine.Config;

public static class APIResultExtention
{
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

    public static IResult Response<T>(this Task<Result<T>> result)
    {
        return result.Result.Response();
    }

    public static IResult Response(this Result result)
    {
        return TypedResults.Ok(result);
    }

    public static IResult Response(this Task<Result> result)
    {
        return result.Result.Response();
    }

    public static IResult Response<T>(this T result)
    {
        return TypedResults.Ok(result);
    }

    public static IResult Response<T>(this Task<T> result)
    {
        return result.Result.Response();
    }
    public static async Task<IResult> ResponseAsync<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask.ConfigureAwait(false);
        if (result == null) return TypedResults.NotFound();
        return result.Status == HttpStatusCode.OK
            ? TypedResults.Ok(result.Value)
            : TypedResults.StatusCode((int)result.Status);
    }

    private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
       TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
       });
}
