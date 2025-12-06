using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for deleting a role.
/// </summary>
public class DeleteRoleRequest
{
    /// <summary>
    /// Gets or sets the role ID string.
    /// </summary>
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; } 
}

/// <summary>
/// Parameters for deleting a role.
/// </summary>
public class DeleteRoleParams
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }
}