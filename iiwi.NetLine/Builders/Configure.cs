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
    public string[] AuthorizationPolicies { get; set; } = [];
    public bool EnableCaching { get; set; } = false;
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
