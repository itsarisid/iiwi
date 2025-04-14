using System.Net;

namespace iiwi.NetLine.Config;

public static class ApiResponseExtention
{
    // Basic object responses
    public static IResult ToOkResult<T>(this T value) => TypedResults.Ok(value);
    public static IResult ToCreatedResult<T>(this T value, string? uri = null) =>
        uri != null ? TypedResults.Created(uri, value) : TypedResults.Ok(value);
    public static IResult ToAcceptedResult<T>(this T value) => TypedResults.AcceptedAtRoute(value: value);

    // Status code specific responses
    public static IResult ToNotFoundResult<T>(this T? value) =>
        value == null ? TypedResults.NotFound() : TypedResults.Ok(value);
    public static IResult ToBadRequestResult<T>(this T value) =>
        TypedResults.BadRequest(value);
    public static IResult ToNoContentResult(this object? _) => TypedResults.NoContent();

    // Specialized success responses
    public static IResult ToFileResult(this byte[] fileBytes, string contentType, string? fileDownloadName = null) =>
        TypedResults.File(fileBytes, contentType, fileDownloadName);
    public static IResult ToStreamResult(this Stream stream, string contentType, string? fileDownloadName = null) =>
        TypedResults.Stream(stream, contentType, fileDownloadName);

    // Error responses
    public static IResult ToErrorResult(this string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest) =>
        TypedResults.Problem(
            title: "An error occurred",
            detail: errorMessage,
            statusCode: (int)statusCode);

    public static IResult ToValidationErrorResult(this IDictionary<string, string[]> errors) =>
        TypedResults.ValidationProblem(errors);

    // Async support
    public static async Task<IResult> ToOkResultAsync<T>(this Task<T> task) =>
        TypedResults.Ok(await task);

    public static async Task<IResult> ToCreatedResultAsync<T>(this Task<T> task, string? uri = null) =>
        uri != null ? TypedResults.Created(uri, await task) : TypedResults.Ok(await task);

    public static async Task<IResult> ToNotFoundResultAsync<T>(this Task<T?> task) =>
        (await task) == null ? TypedResults.NotFound() : TypedResults.Ok(await task);

    // Paged results
    public static IResult ToPagedResult<T>(this IEnumerable<T> items, int pageNumber, int pageSize, long totalCount) =>
        TypedResults.Ok(new PagedResult<T>(items, pageNumber, pageSize, totalCount));

    public record PagedResult<T>(IEnumerable<T> Items, int PageNumber, int PageSize, long TotalCount);

    // Status code wrapper
    public static IResult ToStatusCodeResult<T>(this T value, HttpStatusCode statusCode) =>
        Results.StatusCode((int)statusCode);
}
