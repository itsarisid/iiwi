using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;
using Microsoft.AspNetCore.HttpLogging;
using System.Reflection;
using System.Runtime.InteropServices;

namespace iiwi.NetLine.API;

// Extension method for reusable endpoint mapping
public static class TestEndpointRouteBuilderExtensions
{
    public static void MapTestEndpoint(this IEndpointRouteBuilder endpoints, TestEndpointConfiguration configuration)
    {
        var apiVersionSet = endpoints.CreateApiVersionSet(configuration);
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(PingPong.Group);

        routeGroup.MapGet(BuildEndpointPath(), HandleTestEndpoint).ConfigureTestEndpoint(apiVersionSet, configuration);
    }

    private static ApiVersionSet CreateApiVersionSet(this IEndpointRouteBuilder endpoints, TestEndpointConfiguration config)
    {
        var versionSetBuilder = endpoints.NewApiVersionSet();

        foreach (var deprecatedVersion in config.DeprecatedVersions)
        {
            versionSetBuilder.HasDeprecatedApiVersion(deprecatedVersion);
        }

        foreach (var apiVersion in config.ActiveVersions)
        {
            versionSetBuilder.HasApiVersion(apiVersion);
        }

        return versionSetBuilder.ReportApiVersions().Build();
    }

    private static string BuildEndpointPath()
    {
        return $"v{{version:apiVersion}}{PingPong.TestEndpoint.Endpoint}";
    }

    private static IResult HandleTestEndpoint(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        return TypedResults.Ok(new 
        {
            Author = "Sajid Khan",
            Version = "1.0.0",
            Date = DateTime.Now.ToLongDateString(),
            Time = DateTime.Now.ToLongTimeString(),
            Assembly = Assembly.GetExecutingAssembly().FullName,
            Environment = environment.EnvironmentName,
            Environment.MachineName,
            Framework = RuntimeInformation.FrameworkDescription,
            OS = $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})"
        });
    }

    private static RouteHandlerBuilder ConfigureTestEndpoint(
        this RouteHandlerBuilder builder,
        ApiVersionSet apiVersionSet,
        TestEndpointConfiguration config)
    {
        builder.WithDocumentation(PingPong.TestEndpoint)
               .AllowAnonymous()
               .AddEndpointFilter<ExceptionHandlingFilter>()
               .CacheOutput(config.CachePolicy)
               .WithApiVersionSet(apiVersionSet);

        if (config.EnableHttpLogging)
        {
            builder.WithHttpLogging(HttpLoggingFields.All);
        }

        foreach (var version in config.ActiveVersions)
        {
            builder.MapToApiVersion(version);
        }

        return builder;
    }
}
