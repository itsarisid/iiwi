using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace iiwi.NetLine.Policies;

public class FlexibleAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public FlexibleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options) { }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy == null)
        {
            // Assume policyName is in format "Permission:Action"
            var parts = policyName.Split(':');
            if (parts.Length == 2)
            {
                policy = new AuthorizationPolicyBuilder()
                    .RequireClaim("Permission", policyName)
                    .Build();
            }
        }

        return policy;
    }
}

// Custom authorization handler
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public record PermissionRequirement(string Permission) : IAuthorizationRequirement;
