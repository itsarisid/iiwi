using iiwi.Infrastructure.Email;

namespace iiwi.AppWire.Configurations;

/// <summary>Application settings</summary>
public static class ApplicationSettings
{

    /// <summary>Adds the application settings.</summary>
    /// <param name="builder">The builder.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static WebApplicationBuilder AddApplicationSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddScoped<IMailService, MailService>();
        return builder;
    }
}
