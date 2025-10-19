using Asp.Versioning;
using Asp.Versioning.Conventions;
using iiwi.Common;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;
using Microsoft.AspNetCore.HttpLogging;
using System.Reflection;

using System.Runtime.InteropServices;
namespace iiwi.NetLine.Modules;

/// <summary>
/// Provides test and diagnostic endpoints for the application
/// </summary>
/// <remarks>
/// This module exposes endpoints that return system information and verify application functionality.
/// It includes both public and authenticated endpoints for different testing scenarios.
/// </remarks>
public class TestModule : IEndpoints
{
    /// <summary>
    /// Registers test endpoints with the application's route builder
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure routes</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints parameter is null</exception>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Test.Group);

        var apiVersionSet = endpoints.NewApiVersionSet()
            .HasDeprecatedApiVersion(0.9)
            .HasApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(2, 0))
            .ReportApiVersions()
            .Build();

        /// <summary>
        /// [GET] /test - Public system information endpoint
        /// </summary>
        /// <remarks>
        /// Returns detailed system and application information including:
        /// - Version information
        /// - Runtime environment details
        /// - System metadata
        /// 
        /// This endpoint is publicly accessible and provides basic health check functionality.
        /// </remarks>
        /// <param name="serviceProvider">The application service provider</param>
        /// <returns>System information response</returns>
        /// <response code="200">Returns system information JSON object</response>
        /// <response code="500">If server encounters an error</response>
        routeGroup.MapGet("v{version:apiVersion}" + Test.TestEndpoint.Endpoint,
            IResult (IServiceProvider serviceProvider) => TypedResults.Ok(new
            {
                Auther = "Sajid Khan",
                Version = "1.0.0",
                ActiveVersions = new[] { new ApiVersion(1, 0), new ApiVersion(2, 0) },
                CachePolicy = "DefaultPolicy",
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
            .AddEndpointFilter<ExceptionHandlingFilter>()
            .CacheOutput("DefaultPolicy")
            .WithHttpLogging(HttpLoggingFields.All)
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(1, 0))
            .MapToApiVersion(new ApiVersion(2, 0)); 

        /// <summary>
        /// [GET] /authtest - Authenticated system information endpoint
        /// </summary>
        /// <remarks>
        /// Returns enhanced system information including:
        /// - Version information (v2.0)
        /// - Runtime environment details
        /// - System metadata
        /// 
        /// This endpoint requires authentication and serves to:
        /// 1. Verify authorization infrastructure
        /// 2. Provide additional diagnostics for authenticated users
        /// 
        /// Required permissions: Test.Read
        /// </remarks>
        /// <param name="serviceProvider">The application service provider</param>
        /// <returns>Enhanced system information response</returns>
        /// <response code="200">Returns system information JSON object</response>
        /// <response code="401">If user is not authenticated</response>
        /// <response code="403">If user lacks required permissions</response>
        /// <response code="500">If server encounters an error</response>
        routeGroup.MapGet("v{version:apiVersion}"+Test.AuthTestEndpoint.Endpoint,
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
            .CacheOutput("DefaultPolicy")
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(new ApiVersion(2, 0)); 
    }
}