using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.Model.Enums;
using iiwi.NetLine.Extensions;
namespace iiwi.NetLine.Builders;

public static class EndpointBuilder
{
    public static RouteHandlerBuilder MapVersionedEndpoint<TUrlParams, TRequest, TResponse>(
    this IEndpointRouteBuilder builder,
    Configure<TUrlParams, TRequest, TResponse> configuration)
    where TUrlParams : class, new()
    where TRequest : class, new()
    where TResponse : class, new()
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configuration);

        configuration.RequestDelegate = builder.HandleDelegate<TUrlParams, TRequest, TResponse>(configuration);

        // Explicitly call the two-parameter overload to avoid recursive overload resolution
        return MapVersionedEndpoint<TRequest, TResponse>(builder, (Configure<TRequest, TResponse>)configuration);
    }

    public static RouteHandlerBuilder MapVersionedEndpoint<TRequest, TResponse>(
    this IEndpointRouteBuilder builder,
    Configure<TRequest, TResponse> configuration)
    where TRequest : class, new()
    where TResponse : class, new()
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configuration);

        if(configuration.RequestDelegate == null)
        {
            configuration.RequestDelegate = builder.HandleDelegate(configuration);
        }

        var apiVersionSet = builder.CreateApiVersionSet(configuration);

        var httpMethodName = Enum.GetName(configuration.HttpMethod) ?? throw new ArgumentException($"Invalid HttpMethod: {configuration.HttpMethod}", nameof(configuration));
        var endpoint = builder.MapMethods(
            configuration.BuildEndpointPath(),
            [httpMethodName],
            configuration.RequestDelegate);

        configuration.AdditionalConfiguration?.Invoke(endpoint);

        return new EndpointConfigurator<TRequest, TResponse>(endpoint, configuration, apiVersionSet)
            .ApplyMetadata()
            .ApplyAuthentication()
            .ApplyCaching()
            .ApplyLogging()
            .ApplyFilters()
            .ApplyVersioning()
            .Builder;
    }
}

public class EndpointConfigurator<TRequest, TResponse>(
    RouteHandlerBuilder builder,
    Configure<TRequest, TResponse> configuration,
    ApiVersionSet apiVersionSet)
    where TRequest : class, new()
    where TResponse : class, new()
{
    /// <summary>
    /// Gets the Builder.
    /// </summary>
    public RouteHandlerBuilder Builder { get; } = builder;
    /// <summary>
    /// Gets the Configuration.
    /// </summary>
    public Configure<TRequest, TResponse> Configuration { get; } = configuration;
    /// <summary>
    /// Gets the ApiVersionSet.
    /// </summary>
    public ApiVersionSet ApiVersionSet { get; } = apiVersionSet;

    /// <summary>
    /// Executes the ApplyMetadata operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyMetadata()
    {
        Builder.WithMetadata(Configuration.EndpointMetadata)
               .WithDocumentation(Configuration.EndpointDetails);
        return this;
    }

    /// <summary>
    /// Executes the ApplyAuthentication operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyAuthentication()
    {
        if (Configuration.AuthorizationPolicies.Length > 0)
        {
            Builder.RequireAuthorization(Configuration.AuthorizationPolicies);
        }
        else
        {
            Builder.AllowAnonymous();
        }
        return this;
    }

    /// <summary>
    /// Executes the ApplyCaching operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyCaching()
    {
        if (Configuration.EnableCaching && Configuration.CachePolicy != CachePolicy.NoCache)
        {
            Builder.CacheOutput(Configuration.CachePolicy.ToString());
        }
        return this;
    }

    /// <summary>
    /// Executes the ApplyLogging operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyLogging()
    {
        if (Configuration.EnableHttpLogging)
        {
            Builder.WithHttpLogging(Configuration.HttpLoggingFields);
        }
        return this;
    }

    /// <summary>
    /// Executes the ApplyFilters operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyFilters()
    {
        if (Configuration.EndpointFilters?.Any() == true)
        {
            Builder.AddFiltersByNames(Configuration.EndpointFilters);
        }
        return this;
    }

    /// <summary>
    /// Executes the ApplyVersioning operation.
    /// </summary>
    public EndpointConfigurator<TRequest, TResponse> ApplyVersioning()
    {
        Builder.WithApiVersionSet(ApiVersionSet);

        foreach (var version in Configuration.ActiveVersions)
        {
            Builder.MapToApiVersion(version);
        }

        return this;
    }
}
