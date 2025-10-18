using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.Model.Enums;
using iiwi.NetLine.Extensions;

namespace iiwi.NetLine.Builders;

// Main generic endpoint builder
public static class GenericEndpointBuilder
{
    public static void MapVersionedEndpoint<TEndpoint, TResponse>(
        this IEndpointRouteBuilder endpoints,
        EndpointConfiguration<TEndpoint, TResponse> configuration)
        where TEndpoint : class
        where TResponse : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentNullException.ThrowIfNull(configuration);

        var apiVersionSet = endpoints.CreateApiVersionSet(configuration);
        var httpMethodName = Enum.GetName(configuration.HttpMethod) ?? throw new ArgumentException($"Invalid HttpMethod: {configuration.HttpMethod}", nameof(configuration));

        var builder = endpoints.MapMethods(
            configuration.BuildEndpointPath(),
            [httpMethodName],
            configuration.RequestDelegate)
            .ConfigureEndpoint(apiVersionSet, configuration);

        configuration.AdditionalConfiguration?.Invoke(builder);
    }

    private static ApiVersionSet CreateApiVersionSet<TEndpoint, TResponse>(
        this IEndpointRouteBuilder endpoints,
        EndpointConfiguration<TEndpoint, TResponse> configuration)
        where TEndpoint : class
        where TResponse : class, new()
    {
        var versionSetBuilder = endpoints.NewApiVersionSet();

        foreach (var deprecatedVersion in configuration.DeprecatedVersions)
        {
            versionSetBuilder.HasDeprecatedApiVersion(deprecatedVersion);
        }

        foreach (var apiVersion in configuration.ActiveVersions)
        {
            versionSetBuilder.HasApiVersion(apiVersion);
        }

        return versionSetBuilder.ReportApiVersions().Build();
    }

    private static RouteHandlerBuilder ConfigureEndpoint<TEndpoint, TResponse>(
        this RouteHandlerBuilder builder,
        ApiVersionSet apiVersionSet,
        EndpointConfiguration<TEndpoint, TResponse> configuration)
        where TEndpoint : class
        where TResponse : class, new()
    {
        builder.WithMetadata(configuration.EndpointMetadata)
               .WithDocumentation(configuration.EndpointDetails)
               .WithApiVersionSet(apiVersionSet);

        if (configuration.RequireAuthorization)
        {
            builder.RequireAuthorization(configuration.AuthorizationPolicies);
        }
        else
        {
            builder.AllowAnonymous();
        }

        if (configuration.EnableCaching && configuration.CachePolicy != CachePolicy.NoCache)
        {
            builder.CacheOutput(nameof(configuration.CachePolicy));
        }

        if (configuration.EnableHttpLogging)
        {
            builder.WithHttpLogging(configuration.HttpLoggingFields);
        }

        //foreach (var filter in configuration.EndpointFilters)
        //{
        //    builder.AddEndpointFilter(filter);
        //}
        builder.AddFiltersByNames(configuration.EndpointFilters);

        foreach (var version in configuration.ActiveVersions)
        {
            builder.MapToApiVersion(version);
        }

        return builder;
    }

    private static void AddFiltersByNames(this RouteHandlerBuilder builder, IEnumerable<string> filterNames)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        foreach (var filterName in filterNames)
        {
            // Search for the type by its full name within the current assembly.
            var filterType = Type.GetType($"{assemblyName}.Filters.{filterName}");

            if (filterType != null && typeof(IEndpointFilter).IsAssignableFrom(filterType))
            {
                // Instead of creating the instance here, create a factory delegate.
                // This delegate will be executed by the runtime when the endpoint is invoked.
                builder.AddEndpointFilter(async (context, next) =>
                {
                    // Resolve the filter instance using the request's service provider.
                    var filterInstance = (IEndpointFilter)context.HttpContext.RequestServices.GetRequiredService(filterType);

                    // Now invoke the filter's logic.
                    return await filterInstance.InvokeAsync(context, next);
                });
            }
            else
            {
                Console.WriteLine($"Filter type '{filterName}' not found or does not implement IEndpointFilter.");
            }
        }
    }
}
