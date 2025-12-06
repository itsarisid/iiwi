using System.Text.Json.Serialization;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for getting role claims.
/// </summary>
public class GetRoleClaimsRequest
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    [JsonIgnore]
    public int RoleId { get; set; }
}

/// <summary>
/// Parameters for getting role claims.
/// </summary>
public class GetRoleClaimsParams
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    public int RoleId { get; set; }
}
