using iiwi.Library;
using static iiwi.Library.Helper;

namespace iiwi.Application.Authorization;

/// <summary>
/// Request model for adding a claim to a role.
/// </summary>
public class AddRoleClaimRequest
{
    /// <summary>
    /// Gets or sets the role ID.
    /// </summary>
    [FromUrl]
    public int RoleId { get; init; }

    /// <summary>
    /// Gets or sets the claim type.
    /// </summary>
    public string ClaimType { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the claim value.
    /// </summary>
    public string ClaimValue { get; init; } = string.Empty;
}
