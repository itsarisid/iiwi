using iiwi.Application;
using iiwi.Application.Provider;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.Name = "iiwi.Cookie";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = new PathString("/api/v1.0/auth/login");
            options.LogoutPath = new PathString("/api/v1.0/auth/logout");
            options.AccessDeniedPath = new PathString("/api/v1.0/auth/denied");
            options.SlidingExpiration = true;
        });

        return services;
    }
}
