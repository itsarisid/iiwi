using Asp.Versioning;
using Microsoft.AspNetCore.HttpLogging;

namespace iiwi.NetLine.Endpoints;

public abstract class Endpoint<TRequest, TResponse>
    where TRequest : class, new()
    where TResponse : class, new()
{
    // Configuration
    public abstract string RoutePattern { get; }
    public abstract IEnumerable<string> HttpMethods { get; }
    public abstract object Documentation { get; }
    public abstract ApiVersion[] ActiveVersions { get; }

    // Optional configuration with defaults
    public virtual string RouteGroupPrefix => string.Empty;
    public virtual string GroupName => "API";
    public virtual double[] DeprecatedVersions => Array.Empty<double>();
    public virtual bool RequireAuthorization => false;
    public virtual string[] AuthorizationPolicies => Array.Empty<string>();
    public virtual bool EnableCaching => false;
    public virtual string CachePolicy => "DefaultPolicy";
    public virtual bool EnableHttpLogging => false;
    public virtual HttpLoggingFields HttpLoggingFields => HttpLoggingFields.All;
    public virtual IEndpointFilter[] EndpointFilters => Array.Empty<IEndpointFilter>();
    public virtual List<object> EndpointMetadata => new();

    // Abstract method to handle the request
    public abstract Task<TResponse> HandleAsync(TRequest request, IServiceProvider serviceProvider, CancellationToken cancellationToken = default);

    // Virtual method for additional configuration
    public virtual void ConfigureEndpoint(RouteHandlerBuilder builder) { }

    // Build full endpoint path
    public string BuildEndpointPath() => $"v{{version:apiVersion}}{RoutePattern}";
}
