using iiwi.Model;
using iiwi.Model.Endpoints;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Contains endpoints for testing and application metadata
/// </summary>
public class PingPong
{
    /// <summary>
    /// Group information for Test endpoints
    /// </summary>
    /// <remarks>
    /// This group contains endpoints used for testing API connectivity and retrieving basic application metadata.
    /// These endpoints are useful for health checks and integration testing.
    /// </remarks>
    public static EndpointDetails Group => new()
    {
        Name = "Ping Pong",
        Tags = "Ping Pong",
        Summary = "Ping Pong endpoints",
        Description = "Collection of endpoints for testing API connectivity and retrieving application metadata."
    };

    /// <summary>
    /// Basic connectivity test endpoint
    /// </summary>
    /// <remarks>
    /// This endpoint verifies that the API is running and responsive.
    /// Returns basic application metadata when successful.
    /// Does not require authentication.
    /// </remarks>
    public static EndpointDetails TestEndpoint => new()
    {
        Endpoint = "/ping-pong",
        Name = "Ping Pong",
        Summary = "API connectivity PingPong",
        Description = "This API endpoint can be used for testing basic connectivity and retrieving application meta data."
    };

    public static EndpointDetails SystemInfoEndpoint => new()
    {
        Endpoint = "/system/info",
        Name = "system info",
        Summary = "API connectivity PingPong",
        Description = "This API endpoint can be used for testing basic connectivity and retrieving application meta data."
    };


    /// <summary>
    /// Authenticated endpoint test
    /// </summary>
    /// <remarks>
    /// This endpoint verifies both API connectivity and successful authentication.
    /// Requires valid authentication credentials.
    /// Returns application metadata including user-specific information when successful.
    /// </remarks>
    public static EndpointDetails AuthTestEndpoint => new()
    {
        Endpoint = "/auth-ping-pong",
        Name = "Authorized Endpoint Ping Pong",
        Summary = "Authenticated API Ping Pong",
        Description = "This API endpoint verifies both API connectivity and successful authentication."
    };
}