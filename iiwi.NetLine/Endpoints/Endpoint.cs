using Asp.Versioning;
using Microsoft.AspNetCore.HttpLogging;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Base class for API endpoints.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public abstract class Endpoint<TRequest, TResponse>
    where TRequest : class, new()
    where TResponse : class, new()
{
    // Configuration
    /// <summary>
    /// Gets the route pattern.
    /// </summary>
    public abstract string RoutePattern { get; }

    /// <summary>
    /// Gets the HTTP methods.
    /// </summary>
    public abstract IEnumerable<string> HttpMethods { get; }

    /// <summary>
    /// Gets the documentation object.
    /// </summary>
    public abstract object Documentation { get; }

    /// <summary>
    /// Gets the active API versions.
    /// </summary>
    public abstract ApiVersion[] ActiveVersions { get; }

    // Optional configuration with defaults
    /// <summary>
    /// Gets the route group prefix.
    /// </summary>
    public virtual string RouteGroupPrefix => string.Empty;

    /// <summary>
    /// Gets the group name.
    /// </summary>
    public virtual string GroupName => "API";

    /// <summary>
    /// Gets the deprecated API versions.
    /// </summary>
    public virtual double[] DeprecatedVersions => Array.Empty<double>();

    /// <summary>
    /// Gets a value indicating whether authorization is required.
    /// </summary>
    public virtual bool RequireAuthorization => false;

    /// <summary>
    /// Gets the authorization policies.
    /// </summary>
    public virtual string[] AuthorizationPolicies => Array.Empty<string>();

    /// <summary>
    /// Gets a value indicating whether caching is enabled.
    /// </summary>
    public virtual bool EnableCaching => false;

    /// <summary>
    /// Gets the cache policy.
    /// </summary>
    public virtual string CachePolicy => "DefaultPolicy";

    /// <summary>
    /// Gets a value indicating whether HTTP logging is enabled.
    /// </summary>
    public virtual bool EnableHttpLogging => false;

    /// <summary>
    /// Gets the HTTP logging fields.
    /// </summary>
    public virtual HttpLoggingFields HttpLoggingFields => HttpLoggingFields.All;

    /// <summary>
    /// Gets the endpoint filters.
    /// </summary>
    public virtual IEndpointFilter[] EndpointFilters => Array.Empty<IEndpointFilter>();

    /// <summary>
    /// Gets the endpoint metadata.
    /// </summary>
    public virtual List<object> EndpointMetadata => new();

    // Abstract method to handle the request
    /// <summary>
    /// Handles the request asynchronously.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response.</returns>
    public abstract Task<TResponse> HandleAsync(TRequest request, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);

    // Virtual method for additional configuration
    /// <summary>
    /// Configures the endpoint.
    /// </summary>
    /// <param name="builder">The route handler builder.</param>
    public virtual void ConfigureEndpoint(RouteHandlerBuilder builder) { }

    // Build full endpoint path
    /// <summary>
    /// Builds the full endpoint path.
    /// </summary>
    /// <returns>The full endpoint path.</returns>
    public string BuildEndpointPath() => $"v{{version:apiVersion}}{RoutePattern}";
}
