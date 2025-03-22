using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace iiwi.AppWire.Services;

/// <summary>Current User</summary>
/// <remarks>Initializes a new instance of the <see cref="CurrentUser" /> class.</remarks>
/// <param name="httpContextAccessor">The HTTP context accessor.</param>
public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>Gets the identifier.</summary>
    /// <value>The identifier.</value>
    public string Id => _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
}