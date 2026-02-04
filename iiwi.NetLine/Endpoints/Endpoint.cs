using Asp.Versioning;
using Microsoft.AspNetCore.HttpLogging;

namespace iiwi.NetLine.Endpoints;

public abstract class Endpoint<TRequest, TResponse>
    where TRequest : class, new()
    where TResponse : class, new()
{
    // Configuration
    /// <summary>
    /// Gets the RoutePattern.
    /// </summary>
    public abstract string RoutePattern { get; }
    /// <summary>
    /// Gets the HttpMethods.
    /// </summary>
    public abstract IEnumerable<string> HttpMethods { get; }
    /// <summary>
    /// Gets the Documentation.
    /// </summary>
    public abstract object Documentation { get; }
    /// <summary>
    /// Gets the ActiveVersions.
    /// </summary>
    public abstract ApiVersion[] ActiveVersions { get; }

    // Optional configuration with defaults
    /// <summary>
    /// Gets the RouteGroupPrefix.
    /// </summary>
    public virtual string RouteGroupPrefix => string.Empty;
    /// <summary>
    /// Gets the GroupName.
    /// </summary>
    public virtual string GroupName => "API";
    /// <summary>
    /// Gets the DeprecatedVersions.
    /// </summary>
    public virtual double[] DeprecatedVersions => Array.Empty<double>();
    /// <summary>
    /// Gets the RequireAuthorization.
    /// </summary>
    public virtual bool RequireAuthorization => false;
    /// <summary>
    /// Gets the AuthorizationPolicies.
    /// </summary>
    public virtual string[] AuthorizationPolicies => Array.Empty<string>();
    /// <summary>
    /// Gets the EnableCaching.
    /// </summary>
    public virtual bool EnableCaching => false;
    /// <summary>
    /// Gets the CachePolicy.
    /// </summary>
    public virtual string CachePolicy => "DefaultPolicy";
    /// <summary>
    /// Gets the EnableHttpLogging.
    /// </summary>
    public virtual bool EnableHttpLogging => false;
    /// <summary>
    /// Gets the HttpLoggingFields.
    /// </summary>
    public virtual HttpLoggingFields HttpLoggingFields => HttpLoggingFields.All;
    /// <summary>
    /// Gets the EndpointFilters.
    /// </summary>
    public virtual IEndpointFilter[] EndpointFilters => Array.Empty<IEndpointFilter>();
    /// <summary>
    /// Gets the EndpointMetadata.
    /// </summary>
    public virtual List<object> EndpointMetadata => new();

    // Abstract method to handle the request
    /// <summary>
    /// Executes the HandleAsync operation.
    /// </summary>
    public abstract Task<TResponse> HandleAsync(TRequest request, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);

    // Virtual method for additional configuration
    /// <summary>
    /// Executes the ConfigureEndpoint operation.
    /// </summary>
    public virtual void ConfigureEndpoint(RouteHandlerBuilder builder) { }

    // Build full endpoint path
    /// <summary>
    /// Executes the BuildEndpointPath operation.
    /// </summary>
    public string BuildEndpointPath() => $"v{{version:apiVersion}}{RoutePattern}";
}
