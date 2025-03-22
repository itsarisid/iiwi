using iiwi.AppWire.Services;

namespace iiwi.AppWire.Configurations;

/// <summary>App Services DI</summary>
public static class DependencyInjection
{
    /// <summary>Adds the web services.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        return services;
    }
}
