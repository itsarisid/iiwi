
namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for adding a role.
/// </summary>
public class AddRoleRequest
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
