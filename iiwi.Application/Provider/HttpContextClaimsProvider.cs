using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace iiwi.Application.Provider;

/// <summary>
/// Provides claims from the HTTP context.
/// </summary>
/// <param name="httpContext">The HTTP context accessor.</param>
public class HttpContextClaimsProvider(IHttpContextAccessor httpContext) : IClaimsProvider
{
    /// <summary>
    /// Gets the claims principal.
    /// </summary>
    public ClaimsPrincipal ClaimsPrinciple { get; private set; } = httpContext?.HttpContext?.User;
}
