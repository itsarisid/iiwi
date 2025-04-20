namespace iiwi.NetLine.Filters;

// Define the ExceptionHandlingFilter class that implements the IEndpointFilter interface
public class ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> _logger) : IEndpointFilter
{
    // Implement the InvokeAsync method from the IEndpointFilter interface
    // This method is called when the filter is applied to an endpoint
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            // Call the next filter or endpoint in the pipeline and return the result
            return await next(context);
        }
        catch (Exception ex)
        {
            // Log the exception using the logger
            _logger.LogError(ex, "An unhandled exception occurred while processing the request");
            // Return a standardized error response
            return Results.Problem("An unexpected error occurred. Please try again later.");
        }
    }
}
