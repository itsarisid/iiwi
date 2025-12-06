using iiwi.Model;
using iiwi.Model.Endpoints;

namespace iiwi.NetLine.Endpoints;

/// <summary>
/// Contains endpoints for testing and application metadata
/// </summary>
public class Test
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
        Name = "Test",
        Tags = "Test",
        Summary = "Test endpoints",
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
        Endpoint = "/test",
        Name = "Test",
        Summary = "API connectivity test",
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
        Endpoint = "/auth-test",
        Name = "Authorized Endpoint Test",
        Summary = "Authenticated API test",
        Description = "This API endpoint verifies both API connectivity and successful authentication."
    };
}