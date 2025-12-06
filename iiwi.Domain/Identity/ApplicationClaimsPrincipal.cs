using System.Security.Claims;

namespace iiwi.Domain.Identity;

/// <summary>
/// Represents an application claims principal.
/// </summary>
/// <param name="principal">The claims principal.</param>
public class ApplicationClaimsPrincipal(ClaimsPrincipal principal) : ClaimsPrincipal(principal)
{
    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public int UserId => int.Parse(FindFirst(ClaimTypes.Sid).Value);
}
