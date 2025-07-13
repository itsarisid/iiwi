using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace iiwi.Application;

public class ClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, optionsAccessor)
{
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
