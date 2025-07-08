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
        Name = "Add Role",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails UpdateRole => new()
    {
        Endpoint = "/role/{id}",
        Name = "Update Role",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails DeleteRole => new()
    {
        Endpoint = "/role/{id}",
        Name = "Delete Role",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails RolesById => new()
    {
        Endpoint = "role/{id}",
        Name = "Search Role",
        Summary = "Search role by id",
        Description = ""
    };

    public static EndpointDetails AddClaim => new()
    {
        Endpoint = "/claim",
        Name = "Add Claim",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails UpdateClaim => new()
    {
        Endpoint = "/claim/{id}",
        Name = "Update Claim",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails DeleteClaim => new()
    {
        Endpoint = "/claim/{id}",
        Name = "Delete Claim",
        Summary = "roles related oprations",
        Description = ""
    };
    
    public static EndpointDetails RemoveRoleClaim => new()
    {
        Endpoint = "{roleId}/claim/{id}",
        Name = "Remove Claim from Role",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails AddRoleClaim => new()
    {
        Endpoint = "{roleId}/claim",
        Name = "Add Claim to Role",
        Summary = "roles related oprations",
        Description = ""
    };
    
    public static EndpointDetails AssignRole => new()
    {
        Endpoint = "/users/assign-role",
        Name = "Assign Role to User",
        Summary = "roles related oprations",
        Description = ""
    };

    public static EndpointDetails AddUserClaim => new()
    {
        Endpoint = "/users/add-claim",
        Name = "Add Claim to User",
        Summary = "roles related oprations",
        Description = ""
    };
    public static EndpointDetails GetRoleClaims => new()
    {
        Endpoint = "/roles/{roleId}/claims",
        Name = "Get all role claims",
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
}
