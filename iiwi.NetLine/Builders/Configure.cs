using Asp.Versioning;
using iiwi.Model;
using iiwi.Model.Enums;
using Microsoft.AspNetCore.HttpLogging;

namespace iiwi.NetLine.Builders;

public class Configure<TEndpoint, TResponse>
    where TEndpoint : class
    where TResponse : class, new()
{
    // Required properties
    public Delegate? RequestDelegate { get; set; }
    public required EndpointDetails EndpointDetails { get; set; }

    public Action<RouteHandlerBuilder>? AdditionalConfiguration { get; set; }
    public ApiVersion[] ActiveVersions { get; set; } = [];
    public double[] DeprecatedVersions { get; set; } = [];

    public HttpVerb HttpMethod { get; set; } = HttpVerb.Get;
    //public bool RequireAuthorization { get; set; } = false; // Deprecated in favor of AuthorizationPolicies
    public string[] AuthorizationPolicies { get; set; } = [];
    public bool EnableCaching { get; set; } = false;
    public bool HasUrlParameters { get; set; } = false;
    public bool HasBody { get; set; } = false;
    public CachePolicy CachePolicy { get; set; } = CachePolicy.NoCache;
    public bool EnableHttpLogging { get; set; } = false;
    public HttpLoggingFields HttpLoggingFields { get; set; } = HttpLoggingFields.All;
    public IEnumerable<string> EndpointFilters { get; set; } = [];
    public List<object> EndpointMetadata { get; set; } = [];

    // Helper method to build full endpoint path
    public string BuildEndpointPath()
    {
        return $"v{{version:apiVersion}}{EndpointDetails.Endpoint}";
    }
}

// Configuration with URL parameters
public class Configure<TUrlParams, TRequest, TResponse>
    : Configure<TRequest, TResponse>
    where TUrlParams : class, new()
    where TRequest : class, new()
    where TResponse : class, new()
{
    // URL parameters specific properties
    public Func<TUrlParams, TRequest, TRequest>? CombineParameters { get; set; }
    public bool ValidateUrlParameters { get; set; } = true;
}