namespace iiwi.NetLine.Filters;

/// <summary>
/// An endpoint filter that provides request/response logging capabilities
/// </summary>
/// <remarks>
/// This filter logs:
/// - Incoming requests before processing
/// - Completed requests after processing
/// 
/// Typical usage scenarios:
/// - Tracking request throughput
/// - Debugging request pipelines
/// - Monitoring endpoint execution
/// 
/// Register via:
/// <code>
/// app.MapGet("/path", () => ...)
///    .AddEndpointFilter&lt;LoggingFilter&gt;();
/// </code>
/// </remarks>
/// <param name="_logger">The logger instance for writing log messages</param>
public class LoggingFilter(ILogger<LoggingFilter> _logger) : IEndpointFilter
{
    /// <summary>
    /// Processes an HTTP request through the filter pipeline
    /// </summary>
    /// <param name="context">The context containing request information</param>
    /// <param name="next">The next filter or endpoint in the pipeline</param>
    /// <returns>The result of processing the request</returns>
    /// <remarks>
    /// <para>
    /// This method:
    /// 1. Logs the incoming request path
    /// 2. Invokes the next pipeline component
    /// 3. Logs the completed request
    /// 4. Returns the result
    /// </para>
    /// <para>
    /// The filter wraps the entire endpoint execution, providing clear
    /// before/after logging boundaries for each request.
    /// </para>
    /// </remarks>
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        // Log request initiation with path
        _logger.LogInformation(
            "Handling request: {RequestPath}",
            context.HttpContext.Request.Path);

        try
        {
            // Proceed through pipeline
            var result = await next(context);

            // Log successful completion
            _logger.LogInformation(
                "Successfully handled request: {RequestPath}",
                context.HttpContext.Request.Path);

            return result;
        }
        catch (Exception ex)
        {
            // Log any pipeline failures
            _logger.LogError(
                ex,
                "Error handling request: {RequestPath}",
                context.HttpContext.Request.Path);
            throw;
        }
    }
}