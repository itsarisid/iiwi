using System.Security.Claims;

namespace iiwi.Application.Provider;

/// <summary>
/// Interface for providing claims.
/// </summary>
public interface IClaimsProvider
{
    /// <summary>
    /// Gets the claims principal.
    /// </summary>
    public ClaimsPrincipal ClaimsPrinciple { get; }
}
