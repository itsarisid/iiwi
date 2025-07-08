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
    public static EndpointDetails AddRole => new()
    {
        Endpoint = "/role",
        Name = "User Role",
        Summary = "roles related oprations",
        Description = ""
    };
    
    public static EndpointDetails Permissions => new()
    {
        Endpoint = "role/{id}/permissions",
        Name = "Search permissions",
        Summary = "Search permissions by role id",
        Description = ""
    };
    
    public static EndpointDetails RolesById => new()
    {
        Endpoint = "role/{id}",
        Name = "Search Role",
        Summary = "Search role by id",
        Description = ""
    };
}
