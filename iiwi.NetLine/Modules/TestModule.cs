using iiwi.Common;
using iiwi.NetLine.Filters;
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
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
       ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Test.Group);

        routeGroup.MapGet(Test.TestEndpoint.Endpoint,
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
         .WithDocumentation(Test.TestEndpoint)
         .AllowAnonymous()
         .AddEndpointFilter<LoggingFilter>()
         .AddEndpointFilter<ExceptionHandlingFilter>()
         .CacheOutput("DefaultPolicy");
        
        routeGroup.MapGet(Test.AuthTestEndpoint.Endpoint,
         IResult (IServiceProvider serviceProvider) => TypedResults.Ok(new
         {
             Auther = "Sajid Khan",
             Version = "2.0.0",
             Date = DateTime.Now.ToLongDateString(),
             Time = DateTime.Now.ToLongTimeString(),
             Assembly = Assembly.GetExecutingAssembly().FullName,
             Environment = serviceProvider.GetRequiredService<IWebHostEnvironment>().EnvironmentName,
             Environment.MachineName,
             Framework = RuntimeInformation.FrameworkDescription,
             OS = $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})",
         }))
         .WithDocumentation(Test.AuthTestEndpoint)
         .AddEndpointFilter<LoggingFilter>()
         .AddEndpointFilter<ExceptionHandlingFilter>()
         .RequireAuthorization(Permissions.Test.Read)
         .CacheOutput("DefaultPolicy");
    }
}
