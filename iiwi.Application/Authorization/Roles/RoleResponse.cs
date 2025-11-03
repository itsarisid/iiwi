namespace iiwi.Application.Authorization;

/// <summary>
/// Response containing a list of roles.
/// </summary>
public record RoleResponse : Response
{
    /// <summary>
    /// Gets or sets the collection of roles returned by an authorization request.
    /// </summary>
    public List<Role> Roles { get; set; }
}

/// <summary>
/// Represents a single role entry returned by the API.
/// </summary>
public record Role
{
    /// <summary>
    /// Gets or sets the role identifier.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets an optional description for the role.
    /// </summary>
    public string? Description { get; set; }
}