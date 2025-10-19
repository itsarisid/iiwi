using Asp.Versioning.Builder;
using iiwi.Model.Enums;
using iiwi.NetLine.Extensions;
namespace iiwi.NetLine.Builders;

public static class EndpointBuilder
{
    public static RouteHandlerBuilder MapVersionedEndpoint<TEndpoint, TResponse>(
    this IEndpointRouteBuilder builder,
    Configure<TEndpoint, TResponse> configuration)
    where TEndpoint : class
    where TResponse : class, new()
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configuration);

        var apiVersionSet = builder.CreateApiVersionSet(configuration);

        var httpMethodName = Enum.GetName(configuration.HttpMethod) ?? throw new ArgumentException($"Invalid HttpMethod: {configuration.HttpMethod}", nameof(configuration));
        var endpoint = builder.MapMethods(
            configuration.BuildEndpointPath(),
            [httpMethodName],
            configuration.RequestDelegate);

        configuration.AdditionalConfiguration?.Invoke(endpoint);

        return new EndpointConfigurator<TEndpoint, TResponse>(endpoint, configuration, apiVersionSet)
            .ApplyMetadata()
            .ApplyAuthentication()
            .ApplyCaching()
            .ApplyLogging()
            .ApplyFilters()
            .ApplyVersioning()
            .Builder;
    }


}
public class EndpointConfigurator<TEndpoint, TResponse>(
    RouteHandlerBuilder builder,
    Configure<TEndpoint, TResponse> configuration,
    ApiVersionSet apiVersionSet)
    where TEndpoint : class
    where TResponse : class, new()
{
    public RouteHandlerBuilder Builder { get; } = builder;
    public Configure<TEndpoint, TResponse> Configuration { get; } = configuration;
    public ApiVersionSet ApiVersionSet { get; } = apiVersionSet;

    public EndpointConfigurator<TEndpoint, TResponse> ApplyMetadata()
    {
        Builder.WithMetadata(Configuration.EndpointMetadata)
               .WithDocumentation(Configuration.EndpointDetails);
        return this;
    }

    public EndpointConfigurator<TEndpoint, TResponse> ApplyAuthentication()
    {
        if (Configuration.RequireAuthorization)
        {
            Builder.RequireAuthorization(Configuration.AuthorizationPolicies);
        }
        else
        {
            Builder.AllowAnonymous();
        }
        return this;
    }

    public EndpointConfigurator<TEndpoint, TResponse> ApplyCaching()
    {
        if (Configuration.EnableCaching && Configuration.CachePolicy != CachePolicy.NoCache)
        {
            Builder.CacheOutput(Configuration.CachePolicy.ToString());
        }
        return this;
    }

    public EndpointConfigurator<TEndpoint, TResponse> ApplyLogging()
    {
        if (Configuration.EnableHttpLogging)
        {
            Builder.WithHttpLogging(Configuration.HttpLoggingFields);
        }
        return this;
    }

    public EndpointConfigurator<TEndpoint, TResponse> ApplyFilters()
    {
        if (Configuration.EndpointFilters?.Any() == true)
        {
            Builder.AddFiltersByNames(Configuration.EndpointFilters);
        }
        return this;
    }

    public EndpointConfigurator<TEndpoint, TResponse> ApplyVersioning()
    {
        Builder.WithApiVersionSet(ApiVersionSet);

        foreach (var version in Configuration.ActiveVersions)
        {
            Builder.MapToApiVersion(version);
        }

        return this;
    }
}

