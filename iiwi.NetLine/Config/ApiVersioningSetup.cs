using static iiwi.NetLine.Extentions.ApiVersioningExtensions;
namespace iiwi.NetLine.Config;

public static class ApiVersioningSetup
{
    /// <summary>
    /// Executes the ApiVersioning operation.
    /// </summary>
    public static IServiceCollection ApiVersioning(this IServiceCollection services, Action<ApiVersioningConfig> configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        var config = new ApiVersioningConfig();
        configure?.Invoke(config);

        services.AddApiVersioning(options => options.ConfigureApiVersioning(config))
                .AddApiExplorer(options => options.ConfigureApiExplorer(config))
                .EnableApiVersionBinding();

        return services;
    }
}