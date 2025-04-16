using iiwi.Application.Provider;
using iiwi.Application;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;

namespace iiwi.NetLine.Config;

public static class AppServicesesSetup
{
    public static IServiceCollection AddAppServiceses(this IServiceCollection services, ConfigurationManager configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        services.AddScoped<IClaimsProvider, HttpContextClaimsProvider>();


        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddScoped<IMailService, MailService>();
        return services;
    }
}
