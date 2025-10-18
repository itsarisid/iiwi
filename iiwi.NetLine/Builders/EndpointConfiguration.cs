using Asp.Versioning;
using iiwi.Model;
using iiwi.Model.Enums;
using Microsoft.AspNetCore.HttpLogging;
using System.Runtime.InteropServices;

namespace iiwi.NetLine.Builders;

// Main configuration class
public class EndpointConfiguration<TEndpoint, TResponse>
    where TEndpoint : class
    where TResponse : class, new()
{
    // Required properties
    public required Delegate RequestDelegate { get; set; }
    public required EndpointDetails EndpointDetails { get; set; }
    public required ApiVersion[] ActiveVersions { get; set; }

    public HttpVerb HttpMethod { get; set; } = HttpVerb.Get;
    public double[] DeprecatedVersions { get; set; } = [];
    public bool RequireAuthorization { get; set; } = false;
    public string[] AuthorizationPolicies { get; set; } = [];
    public bool EnableCaching { get; set; } = false;
    public CachePolicy CachePolicy { get; set; } = CachePolicy.NoCache;
    public bool EnableHttpLogging { get; set; } = false;
    public HttpLoggingFields HttpLoggingFields { get; set; } = HttpLoggingFields.All;
    public IEnumerable<string> EndpointFilters { get; set; } = [];
    public List<object> EndpointMetadata { get; set; } = [];
    public Action<RouteHandlerBuilder>? AdditionalConfiguration { get; set; }

    // Helper method to build full endpoint path
    public string BuildEndpointPath()
    {
        return $"v{{version:apiVersion}}{EndpointDetails.Endpoint}";
    }
}

// Response base class for common properties
public abstract class BaseEndpointResponse
{
    public string Version { get; set; } = "1.0.0";
    public string Date => DateTime.Now.ToLongDateString();
    public string Time => DateTime.Now.ToLongTimeString();
    public string? Assembly => System.Reflection.Assembly.GetExecutingAssembly().FullName;
    public string MachineName => Environment.MachineName;
    public string Framework => RuntimeInformation.FrameworkDescription;
    public string OS => $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})";
}

// Specific response types
public class SystemInfoResponse : BaseEndpointResponse
{
    public string Author { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
}

public class HealthCheckResponse : BaseEndpointResponse
{
    public string Status { get; set; } = "Healthy";
    public DateTimeOffset CheckedAt { get; set; } = DateTimeOffset.UtcNow;
    public Dictionary<string, string> Services { get; set; } = [];
}
