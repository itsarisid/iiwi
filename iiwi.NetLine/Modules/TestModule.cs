using System.Reflection;
using System.Runtime.InteropServices;

namespace iiwi.NetLine.Modules;

public class TestModule : IEndpoints
{
    /// <summary>  
    /// Configures the "/test" endpoint for the application.  
    /// This endpoint provides metadata about the application, including version, environment, and system details.  
    /// </summary>  
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> used to define the route.</param>  
    /// <remarks>  
    /// This endpoint is useful for testing and retrieving application information.  
    /// </remarks>  
    /// <response code="200">Returns application metadata in JSON format.</response>  
    /// <response code="500">If an internal server error occurs.</response>  
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/test",
         IResult (IServiceProvider serviceProvider) => TypedResults.Ok(new
         {
             Auther = "Sajid Khan",
             Version = "1.0.0",
             Date = DateTime.Now.ToLongDateString(),
             Time = DateTime.Now.ToLongTimeString(),
             Assembly = Assembly.GetExecutingAssembly().FullName,
             Environment = serviceProvider.GetRequiredService<IWebHostEnvironment>().EnvironmentName,
             Environment.MachineName,
             Framework = RuntimeInformation.FrameworkDescription,
             OS = $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})",
         }))
         .WithTags("Test")
         .WithName("Test")
         .WithSummary("Test endpoint")
         .WithDescription("This API endpoint can be used for testing and retrieving application metadata.")
         .AllowAnonymous()
         .IncludeInOpenApi()
         .CacheOutput("DefaultPolicy");
    }
}
