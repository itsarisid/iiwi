using iiwi.Application;
using iiwi.Application.Provider;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        services.AddIdentityApiEndpoints<ApplicationUser>(options =>
            {
                // Password settings.
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true,
                    RequiredLength = 8,
                    RequiredUniqueChars = 1
                };

                // Lockout settings.
                options.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true
                };

                // Sign in settings
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedAccount = false,
                    RequireConfirmedEmail = false
                };

                // User settings.
                options.User = new UserOptions
                {
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.@1234567890!#$%&'*+-/=?^_`{|}~",
                    RequireUniqueEmail = true
                };
            })
            .AddRoles<ApplicationRole>()
            .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();
        return services;
    }
}
