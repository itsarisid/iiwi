using Audit.EntityFramework;
using Audit.EntityFramework.Interceptors;
using DotNetCore.EntityFrameworkCore;
using iiwi.Database;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace iiwi.NetLine.Config;

/// <summary>Identity Setup</summary>
public static class IdentitySetup
{
    /// <summary>Adds the application settings.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddIdentity(this IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddContext<iiwiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(iiwiDbContext))));
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(iiwiDbContext)))
                .AddInterceptors([new AuditSaveChangesInterceptor(),
                new AuditCommandInterceptor()
                {
                    // Configure the audit command interceptor.
                    AuditEventType = "{database}-{context}",
                    IncludeReaderResults = true
                    
                }]));
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
                RequireConfirmedEmail = false,
                RequireConfirmedPhoneNumber = false
            };

            // User settings.
            options.User = new UserOptions
            {
                AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.@1234567890!#$%&'*+-/=?^_`{|}~",
                RequireUniqueEmail = true
            };
        })
         .AddRoles<ApplicationRole>()
         .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
}
