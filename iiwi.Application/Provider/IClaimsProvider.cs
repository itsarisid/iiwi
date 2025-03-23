using System.Security.Claims;

namespace iiwi.Application.Provider;

public interface IClaimsProvider
{
    public ClaimsPrincipal ClaimsPrinciple { get; }
}
