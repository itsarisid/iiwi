using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for removing a claim.
/// </summary>
public class RemoveClaimRequest
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    [JsonIgnore]
    public int RoleId { get; set; }
}

/// <summary>
/// Parameters for removing a claim.
/// </summary>
public class RemoveClaimParams
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    public int RoleId { get; set; }
}
