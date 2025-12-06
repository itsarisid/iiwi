using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for updating a role.
/// </summary>
public class UpdateRoleRequest
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonIgnore]
    public  int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; }
}

/// <summary>
/// Parameters for updating a role.
/// </summary>
public class UpdateRoleParams
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }
}
