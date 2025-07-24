namespace iiwi.NetLine.Filters;

/// <summary>
/// Global exception handling filter for API endpoints
/// </summary>
/// <remarks>
/// This filter provides centralized exception handling by:
/// 1. Catching unhandled exceptions in the request pipeline
/// 2. Logging detailed error information
/// 3. Returning standardized error responses
/// 
/// Prevents sensitive exception details from leaking to clients while
/// ensuring errors are properly logged for diagnostics.
/// </remarks>
/// <param name="_logger">The logger instance for error logging</param>
public class ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> _logger) : IEndpointFilter
{
    /// <summary>
    /// Processes requests while handling any exceptions that occur
    /// </summary>
    /// <param name="context">The endpoint invocation context</param>
    /// <param name="next">Delegate to the next filter in the pipeline</param>
    /// <returns>
    /// Either the successful result or a ProblemDetails response on failure
    /// </returns>
    /// <remarks>
    /// <para>
    /// Execution flow:
    /// 1. Attempts to execute the request pipeline
    /// 2. On success: returns the pipeline's result
    /// 3. On failure:
    ///    - Logs full exception details (including stack trace)
    ///    - Returns a user-friendly 500 error response
    ///    - Preserves original error for correlation
    /// </para>
    /// <para>
    /// The standardized error response includes:
    /// - HTTP 500 status code
    /// - Generic error message
    /// - Correlation identifiers (when available)
    /// </para>
    /// </remarks>
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        try
        {
            // Proceed through the pipeline
            return await next(context);
        }
        catch (Exception ex)
        {
            // Log full exception details including:
            // - Exception type
            // - Message
            // - Stack trace
            // - Request context
            _logger.LogError(
                ex,
                "Unhandled exception processing {RequestPath}",
                context.HttpContext.Request.Path);

            // Return standardized error response that:
            // - Doesn't expose implementation details
            // - Provides consistent error format
            // - Includes correlation information
            return Results.Problem(
                title: "An unexpected error occurred",
                detail: "Please try again later. Reference: " + context.HttpContext.TraceIdentifier,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}