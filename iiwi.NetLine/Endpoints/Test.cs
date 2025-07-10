using iiwi.Model;

namespace iiwi.NetLine.Endpoints;

public class Test
{
    public static EndpointDetails Group => new()
    {
        Name = "Test",
        Tags = "Test",
        Summary = "Test endpoint",
        Description = "This API endpoint can be used for testing and retrieving application meta data."
    };

    public static EndpointDetails TestEndpoint => new()
    {
        Endpoint = "/test",
        Name = "Test",
        Summary = "This API endpoint can be used for testing and retrieving application meta data.",
        Description = "This API endpoint can be used for testing and retrieving application meta data."
    };
    
    public static EndpointDetails AuthTestEndpoint => new()
    {
        Endpoint = "/auth-test",
        Name = "Authorized End point Test",
        Summary = "This API endpoint can be used for testing and retrieving application meta data.",
        Description = "This API endpoint can be used for testing and retrieving application meta data."
    };
}
