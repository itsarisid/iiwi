using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace iiwi.Application;

/// <summary>
/// Custom factory for creating claims principals for application users.
/// Adds user and role claims, and authentication method claims.
/// </summary>
/// <param name="userManager">The user manager.</param>
/// <param name="roleManager">The role manager.</param>
/// <param name="optionsAccessor">The identity options accessor.</param>
public class ClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, optionsAccessor)
{
    /// <summary>
    /// Creates a claims principal for the specified user asynchronously.
    /// </summary>
    /// <param name="user">The user to create the principal for.</param>
    /// <summary>
    /// Creates a ClaimsPrincipal for the specified user augmented with the user's claims, role claims, and an authentication method (AMR) claim.
    /// </summary>
    /// <param name="user">The application user for whom to create the principal.</param>
    /// <returns>The created ClaimsPrincipal containing the user's claims, role claims, and an updated "amr" claim.</returns>
    /// <summary>
    /// Creates a ClaimsPrincipal for the specified user including the user's claims, claims derived from their roles, and an authentication method ("amr") claim.
    /// </summary>
    /// <returns>A ClaimsPrincipal augmented with the user's claims, role-derived claims, and an "amr" claim indicating the authentication method.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="user"/> is null.</exception>
    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);

        // Get base principal with standard claims
        var principal = await base.CreateAsync(user);

        if (principal.Identity is not ClaimsIdentity identity)
        {
            return principal;
        }

        // Get all claims and roles separately
        var userClaims = await UserManager.GetClaimsAsync(user);
        var userRoles = await UserManager.GetRolesAsync(user);

        // Process user claims
        await AddUniqueClaimsAsync(identity, userClaims);

        // Process role claims
        var roleClaims = await GetRoleClaimsAsync(userRoles);
        await AddUniqueClaimsAsync(identity, roleClaims);

        // Add authentication method claim
        AddAuthenticationMethodClaim(identity, user);

        return principal;
    }

    private async Task<List<Claim>> GetRoleClaimsAsync(IEnumerable<string> roleNames)
    {
        var roleClaims = new List<Claim>();

        foreach (var roleName in roleNames)
        {
            var role = await RoleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var claims = await RoleManager.GetClaimsAsync(role);
                roleClaims.AddRange(claims);
            }
        }

        return roleClaims;
    }

    private static Task AddUniqueClaimsAsync(ClaimsIdentity identity, IEnumerable<Claim> claims)
    {
        foreach (var claim in claims)
        {
            if (!identity.HasClaim(c => c.Type == claim.Type && c.Value == claim.Value))
            {
                identity.AddClaim(claim);
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Set or update the authentication method ("amr") claim on the provided identity based on the user's two-factor status.
    /// </summary>
    /// <param name="identity">The ClaimsIdentity to modify.</param>
    /// <param name="user">The ApplicationUser whose TwoFactorEnabled flag determines the amr value ("mfa" for enabled, "pwd" for disabled).</param>
    private static void AddAuthenticationMethodClaim(ClaimsIdentity identity, ApplicationUser user)
    {
        var amrClaim = new Claim(
            "amr",
            user.TwoFactorEnabled ? "mfa" : "pwd");

        // Remove existing amr claim if exists
        var existingAmr = identity.FindFirst("amr");
        if (existingAmr != null)
        {
            identity.RemoveClaim(existingAmr);
        }

        identity.AddClaim(amrClaim);
    }
}