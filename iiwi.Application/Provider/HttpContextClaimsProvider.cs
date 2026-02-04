using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace iiwi.Application.Provider;

public class HttpContextClaimsProvider(IHttpContextAccessor httpContext) : IClaimsProvider
{
    /// <summary>
    /// Gets or sets the ClaimsPrinciple.
    /// </summary>
    public ClaimsPrincipal ClaimsPrinciple { get; private set; } = httpContext?.HttpContext?.User;
}
