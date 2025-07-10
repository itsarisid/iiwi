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
    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        //await userManager.AddClaimAsync(user, new Claim("Permission", "Test.Read"));

        //var role = await roleManager.FindByNameAsync("Test");
        //await roleManager.AddClaimAsync(role, new Claim("Permission", "Test.Read"));

        //var claims = new List<Claim>();
        var claims = await userManager.GetClaimsAsync(user);
        foreach (var claim in claims)
        {
            if (!identity.HasClaim(c => c.Type == claim.Type && c.Value == claim.Value))
            {
                claims.Add(claim);
            }
        }

        if (user.TwoFactorEnabled)
        {
            claims.Add(new Claim("amr", "mfa"));
        }
        else
        {
            claims.Add(new Claim("amr", "pwd"));
        }

        //claims.Add(new Claim("Permission", "Test.Read"));
        identity?.AddClaims(claims);
        return principal;
    }
}
