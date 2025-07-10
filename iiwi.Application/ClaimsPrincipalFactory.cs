using iiwi.Domain.Identity;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace iiwi.Application;

public class ClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, optionsAccessor)
{
    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        //await userManager.AddClaimAsync(user, new Claim("Permission", "Test.Read"));

        //var role = await roleManager.FindByNameAsync("Test");
        //await roleManager.AddClaimAsync(role, new Claim("Permission", "Test.Read"));

        //var claims = new List<Claim>();
        var allClaims = await userManager.GetClaimsAsync(user)?? [];
        foreach (var claim in allClaims)
        {
            if (!identity.HasClaim(c => c.Type == claim.Type && c.Value == claim.Value))
            {
                allClaims.Add(claim);
            }
        }

        var userRoles = await userManager.GetRolesAsync(user);
        // Get claims from user's roles
        var roleClaims = new List<Claim>();

        foreach (var roleName in userRoles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var claims = await roleManager.GetClaimsAsync(role);
                allClaims.AddRange(claims);
            }
        }

        if (user.TwoFactorEnabled)
        {
            allClaims.Add(new Claim("amr", "mfa"));
        }
        else
        {
            allClaims.Add(new Claim("amr", "pwd"));
        }

        //claims.Add(new Claim("Permission", "Test.Read"));
        identity?.AddClaims(allClaims);
        return principal;
    }
}
