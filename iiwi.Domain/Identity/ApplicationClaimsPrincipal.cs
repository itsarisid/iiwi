using System.Security.Claims;

namespace iiwi.Domain.Identity;

public class ApplicationClaimsPrincipal(ClaimsPrincipal principal) : ClaimsPrincipal(principal)
{
    /// <summary>
    /// Gets the UserId.
    /// </summary>
    public int UserId => int.Parse(FindFirst(ClaimTypes.Sid).Value);
}
