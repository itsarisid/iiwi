using iiwi.Model;

public static class Authorization
{
    public static EndpointDetails Group => new()
    {
        Name = "Authorization",
        Tags = "Authorization",
        Summary = "Authorization-related Endpoints",
        Description = "This group contains all the endpoints related to user authorization resource, including roles and permissions."
    };
    public static EndpointDetails AllRoles => new()
    {
        Endpoint = "/roles",
        Name = "roles",
        Summary = "roles",
        Description = ""
    };
    
    public static EndpointDetails RolesById => new()
    {
        Endpoint = "/{id}",
        Name = "roles",
        Summary = "roles",
        Description = ""
    };
}
