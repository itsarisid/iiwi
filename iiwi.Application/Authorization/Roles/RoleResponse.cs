namespace iiwi.Application.Authorization;

/// <summary>
/// Response model for role.
/// </summary>
public record RoleResponse : Response
{
    /// <summary>
    /// Gets or sets the list of roles.
    /// </summary>
    public List<Role> Roles { get; set; }
}

/// <summary>
/// Role model.
/// </summary>
public record Role
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; }
}