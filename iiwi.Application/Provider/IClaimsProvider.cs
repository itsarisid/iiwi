using System.Security.Claims;

namespace iiwi.Application.Provider;

public interface IClaimsProvider
{
    /// <summary>
    /// Gets the ClaimsPrinciple.
    /// </summary>
    public ClaimsPrincipal ClaimsPrinciple { get; }
}
