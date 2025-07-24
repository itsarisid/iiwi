namespace iiwi.NetLine.Config;

/// <summary>
/// Provides extension methods for configuring application cookie settings
/// </summary>
/// <remarks>
/// This configuration sets up secure defaults for authentication cookies
/// used by ASP.NET Core Identity and other authentication handlers.
/// </remarks>
public static class CookieSetup
{
    /// <summary>
    /// Configures application cookie settings with secure defaults
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection</returns>
    /// <remarks>
    /// <para>
    /// Configures the following cookie settings:
    /// 1. <b>Name</b>: "iiwi.Cookie" - Identifies the application's auth cookie
    /// 2. <b>HttpOnly</b>: true - Prevents client-side script access
    /// 3. <b>Expiration</b>: 5 minutes sliding window
    /// 4. <b>Sliding Expiration</b>: true - Renews expiration on activity
    /// </para>
    /// <para>
    /// Security Considerations:
    /// - HttpOnly prevents XSS attacks from accessing cookies
    /// - Short expiration limits exposure from cookie theft
    /// - Sliding expiration balances security and usability
    /// </para>
    /// <para>
    /// Usage Example:
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.Services.AddAppCookies();
    /// </code>
    /// </para>
    /// </remarks>
    public static IServiceCollection AddAppCookies(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.ConfigureApplicationCookie(options =>
        {
            // Unique cookie name for the application
            options.Cookie.Name = "iiwi.Cookie";

            // Security settings
            options.Cookie.HttpOnly = true;

            // Session management
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            options.SlidingExpiration = true;
        });

        return services;
    }
}