using DotNetCore.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection.Metadata;

namespace iiwi.NetLine.Config;

public static class APIResultExtention
{
    public static IResult MyResult<T>(this Result<T> result)
    {
        if (result == null)
        {
            return TypedResults.NotFound();
        }
        if (result.Status == HttpStatusCode.OK)
        {
            return TypedResults.Ok(result.Value);
        }
        return TypedResults.Empty;
    }


    public static async Task<TResult> Convert<TSource, TResult>(
    this Task<TSource> task, Func<TSource, TResult> projection)
    {
        var result = await task.ConfigureAwait(false);
        return projection(result);
    }

    public static IResult MyResult<T>(this Task<Result<T>> result)
    {
        return result.Result.MyResult();
    }

    public static IResult MyResult(this Result result)
    {
        //return new ObjectResult(result.Message)
        //{
        //    StatusCode = (int)result.Status
        //};
        return TypedResults.Ok(result);
    }

    public static IResult MyResult(this Task<Result> result)
    {
        return result.Result.MyResult();
    }

    public static IResult MyResult<T>(this T result)
    {
        return TypedResults.Ok(result);
    }

    public static IResult MyResult<T>(this Task<T> result)
    {
        return result.Result.MyResult();
    }

    private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
        TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
        });
}
