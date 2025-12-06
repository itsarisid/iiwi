
using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for adding a claim.
/// </summary>
public class AddClaimRequest
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    [JsonIgnore]
    public int RoleId { get; set; }
}

/// <summary>
/// Parameters for adding a claim.
/// </summary>
public class AddClaimParams
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    public int RoleId { get; set; }
}
