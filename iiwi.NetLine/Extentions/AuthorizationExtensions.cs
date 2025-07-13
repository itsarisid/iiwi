using iiwi.Application;
using iiwi.Common;
using iiwi.Database;

namespace iiwi.NetLine.Extentions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAutoPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Register from static permissions class
            foreach (var permission in Permissions.GetAll())
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim(Permissions.Permission, permission));
            }


            // Register from database (if needed)
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var dbPermissions = dbContext.Permission.ToList();

            foreach (var permission in dbPermissions)
            {
                // Fix for CS0229: Explicitly specify the property using the class name
                var codeName = permission.CodeName;

                // Fix for CS0023: Check for null instead of using '!'
                if (options.GetPolicy(codeName) is not null)
                {
                    options.AddPolicy(codeName, policy =>
                        policy.RequireClaim(Permissions.Permission, codeName));
                }
            }
        });

        return services;
    }
}
