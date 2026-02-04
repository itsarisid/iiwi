using Asp.Versioning;
using iiwi.Model;
using iiwi.Model.Enums;
using Microsoft.AspNetCore.HttpLogging;

namespace iiwi.NetLine.Builders;

/// <summary>
/// Defines metadata and configuration for building a versioned API endpoint.
/// </summary>
/// <typeparam name="TEndpoint">The endpoint handler type used to process requests.</typeparam>
/// <typeparam name="TResponse">The response type returned by the endpoint.</typeparam>
public class Configure<TEndpoint, TResponse>
    where TEndpoint : class
    where TResponse : class, new()
{
    /// <summary>
    /// The delegate invoked when the endpoint executes.
    /// </summary>
    public Delegate? RequestDelegate { get; set; }

    /// <summary>
    /// Documentation and routing details used for endpoint metadata and OpenAPI.
    /// </summary>
    public required EndpointDetails EndpointDetails { get; set; }

    /// <summary>
    /// Additional per-endpoint configuration applied to the route handler.
    /// </summary>
    public Action<RouteHandlerBuilder>? AdditionalConfiguration { get; set; }

    /// <summary>
    /// API versions that this endpoint supports.
    /// </summary>
    public ApiVersion[] ActiveVersions { get; set; } = [];

    /// <summary>
    /// API versions that are deprecated for this endpoint.
    /// </summary>
    public double[] DeprecatedVersions { get; set; } = [];

    /// <summary>
    /// The HTTP verb associated with the endpoint.
    /// </summary>
    public HttpVerb HttpMethod { get; set; } = HttpVerb.Get;
    //public bool RequireAuthorization { get; set; } = false; // Deprecated in favor of AuthorizationPolicies

    /// <summary>
    /// Authorization policies that must be satisfied to access the endpoint.
    /// </summary>
    public string[] AuthorizationPolicies { get; set; } = [];

    /// <summary>
    /// Indicates whether response caching is enabled for this endpoint.
    /// </summary>
    public bool EnableCaching { get; set; } = false;

    /// <summary>
    /// Indicates whether this endpoint uses URL parameters in its route.
    /// </summary>
    public bool HasUrlParameters { get; set; } = false;

    /// <summary>
    /// Caching policy applied when caching is enabled.
    /// </summary>
    public CachePolicy CachePolicy { get; set; } = CachePolicy.NoCache;

    /// <summary>
    /// Enables HTTP logging for the endpoint when true.
    /// </summary>
    public bool EnableHttpLogging { get; set; } = false;

    /// <summary>
    /// Specifies which HTTP logging fields are captured.
    /// </summary>
    public HttpLoggingFields HttpLoggingFields { get; set; } = HttpLoggingFields.All;

    /// <summary>
    /// Names of endpoint filters to attach to the route handler.
    /// </summary>
    public IEnumerable<string> EndpointFilters { get; set; } = [];

    /// <summary>
    /// Additional metadata objects added to the endpoint.
    /// </summary>
    public List<object> EndpointMetadata { get; set; } = [];

    /// <summary>
    /// Builds the full, versioned route pattern for the endpoint.
    /// </summary>
    /// <returns>The versioned route pattern used by the router.</returns>
    public string BuildEndpointPath()
    {
        return $"v{{version:apiVersion}}{EndpointDetails.Endpoint}";
    }
}

/// <summary>
/// Defines configuration for endpoints that include URL parameters.
/// </summary>
/// <typeparam name="TUrlParams">The URL parameter type.</typeparam>
/// <typeparam name="TRequest">The request payload type.</typeparam>
/// <typeparam name="TResponse">The response type returned by the endpoint.</typeparam>
public class Configure<TUrlParams, TRequest, TResponse>
    : Configure<TRequest, TResponse>
    where TUrlParams : class, new()
    where TRequest : class, new()
    where TResponse : class, new()
{
    /// <summary>
    /// Combines URL parameters with the request payload into a single request instance.
    /// </summary>
    public Func<TUrlParams, TRequest, TRequest>? CombineParameters { get; set; }

    /// <summary>
    /// Enables validation of URL parameters before request handling.
    /// </summary>
    public bool ValidateUrlParameters { get; set; } = true;
}
