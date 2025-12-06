using iiwi.Application;
using iiwi.Application.Provider;
using iiwi.Database.Permissions;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace iiwi.NetLine.Config;

/// <summary>
/// Configures core application services and dependencies
/// </summary>
/// <remarks>
/// This static class centralizes the registration of:
/// - Identity-related services
/// - Authorization components
/// - Email services
/// - Application-specific providers
/// </remarks>
public static class AppServicesSetup
{
    /// <summary>
    /// Registers all application services with the dependency injection container
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <param name="configuration">The application configuration manager</param>
    /// <returns>The configured service collection for method chaining</returns>
    /// <remarks>
    /// <para>
    /// This method performs the following service registrations:
    /// 1. Identity Services:
    ///    - Custom claims principal factory
    ///    - Claims provider for current HTTP context
    /// </para>
    /// <para>
    /// 2. Authorization Services:
    ///    - Permission repository
    ///    - Permission authorization handler
    ///    - Automatic policy registration
    /// </para>
    /// <para>
    /// 3. Email Services:
    ///    - Mail settings configuration
    ///    - Mail service implementation
    /// </para>
    /// <para>
    /// Usage Example:
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.Services.AddAppServices(builder.Configuration);
    /// </code>
    /// </para>
    /// </remarks>
    public static IServiceCollection AddAppServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Register identity-related services
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();

        services.AddSingleton<LoggingFilter>();
        services.AddSingleton<ExceptionHandlingFilter>();

        // Register permission services
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddAutoPolicies();

        // Configure and register email services
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddScoped<IMailService, MailService>();

        return services;
    }
}