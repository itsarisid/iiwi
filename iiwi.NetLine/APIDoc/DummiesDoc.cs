using iiwi.Model;

namespace iiwi.NetLine.APIDoc;

/// <summary>
/// Contains endpoints for testing and application metadata
/// </summary>
public class DummiesDoc
{
  
    public static EndpointDetails Group => new()
    {
        Name = "Dummies",
        Tags = "Dummies",
        Summary = "Dummies endpoints",
        Description = "Collection of endpoints for testing API connectivity and retrieving application metadata."
    };

    
    public static EndpointDetails TestEndpoint => new()
    {
        Endpoint = "/test",
        Name = "Test",
        Summary = "API connectivity Dummies",
        Description = "This API endpoint can be used for testing basic connectivity and retrieving application meta data."
    };

    public static EndpointDetails AuthTestEndpoint => new()
    {
        Endpoint = "/test-ping-pong",
        Name = "Authorized Endpoint Dummies",
        Summary = "Authenticated API Dummies",
        Description = "This API endpoint verifies both API connectivity and successful authentication."
    };
}