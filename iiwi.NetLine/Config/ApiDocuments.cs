using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace iiwi.NetLine.Config;

public static class ApiDocuments
{
    /// <summary>Adds the API documents.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddApiDocuments(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddOpenApi();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "IIWI",
                Description = "IIWI is a mobile-first platform designed to bridge students and counselors through personalized counseling sessions and a collaborative blogging community. Students can discover verified counselors, book sessions, and engage with user-generated content, while counselors showcase their expertise via blogs and manage their professional profiles.",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Sajid Khan",
                    Email = "mysipi@outlook.com",
                    Url = new Uri("https://www.linkedin.com/in/im-sajid-khan/")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt")
                }
            });
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Standard Authorization header using the Bearer scheme (JWT)",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}
