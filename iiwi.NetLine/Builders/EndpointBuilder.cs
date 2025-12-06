using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;
using iiwi.Model.Enums;
using iiwi.Model.Endpoints;
using iiwi.NetLine.Extensions;
namespace iiwi.NetLine.Builders;

/// <summary>
/// Builder for endpoints.
/// </summary>
public static class EndpointBuilder
{
    /// <summary>
    /// Maps a versioned endpoint with URL parameters.
    /// </summary>
    /// <typeparam name="TUrlParams">The URL parameters type.</typeparam>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="builder">The endpoint route builder.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The route handler builder.</returns>
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

    /// <summary>
    /// Maps a versioned endpoint.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    /// <param name="builder">The endpoint route builder.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The route handler builder.</returns>
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

/// <summary>
/// Configurator for endpoints.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class EndpointConfigurator<TRequest, TResponse>(
    RouteHandlerBuilder builder,
    Configure<TRequest, TResponse> configuration,
    ApiVersionSet apiVersionSet)
    where TRequest : class, new()
    where TResponse : class, new()
{
    /// <summary>
    /// Gets the builder.
    /// </summary>
    public RouteHandlerBuilder Builder { get; } = builder;

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    public Configure<TRequest, TResponse> Configuration { get; } = configuration;

    /// <summary>
    /// Gets the API version set.
    /// </summary>
    public ApiVersionSet ApiVersionSet { get; } = apiVersionSet;

    /// <summary>
    /// Applies metadata.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
    public EndpointConfigurator<TRequest, TResponse> ApplyMetadata()
    {
        Builder.WithMetadata(Configuration.EndpointMetadata)
               .WithDocumentation(Configuration.EndpointDetails);
        return this;
    }

    /// <summary>
    /// Applies authentication.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
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
    /// Applies caching.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
    public EndpointConfigurator<TRequest, TResponse> ApplyCaching()
    {
        if (Configuration.EnableCaching && Configuration.CachePolicy != CachePolicy.NoCache)
        {
            Builder.CacheOutput(Configuration.CachePolicy.ToString());
        }
        return this;
    }

    /// <summary>
    /// Applies logging.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
    public EndpointConfigurator<TRequest, TResponse> ApplyLogging()
    {
        if (Configuration.EnableHttpLogging)
        {
            Builder.WithHttpLogging(Configuration.HttpLoggingFields);
        }
        return this;
    }

    /// <summary>
    /// Applies filters.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
    public EndpointConfigurator<TRequest, TResponse> ApplyFilters()
    {
        if (Configuration.EndpointFilters?.Any() == true)
        {
            Builder.AddFiltersByNames(Configuration.EndpointFilters);
        }
        return this;
    }

    /// <summary>
    /// Applies versioning.
    /// </summary>
    /// <returns>The endpoint configurator.</returns>
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
