using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace iiwi.Application.Provider;

public class HttpContextClaimsProvider(IHttpContextAccessor httpContext) : IClaimsProvider
{
    public ClaimsPrincipal ClaimsPrinciple { get; private set; } = httpContext?.HttpContext?.User;
}
