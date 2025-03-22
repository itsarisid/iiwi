using iiwi.Application;
using iiwi.Application.Provider;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace iiwi.AppWire.Configurations;

/// <summary>Identity Setup</summary>
public static class IdentitySetup
{
    /// <summary>Adds the application settings.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();
        return services;
    }
}
