namespace iiwi.NetLine.Config;

public static class CookieSetup
{
    public static IServiceCollection AddAppCookies(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.Name = "iiwi.Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.SlidingExpiration = true;
            });
        return services;
    }
}
