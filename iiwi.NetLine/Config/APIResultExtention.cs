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

    public static async Task<TResult> Convert<TSource, TResult>(
    this Task<TSource> task, Func<TSource, TResult> projection)
    {
        var result = await task.ConfigureAwait(false);
        return projection(result);
    }
}
