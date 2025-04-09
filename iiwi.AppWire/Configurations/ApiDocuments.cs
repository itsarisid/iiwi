using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors.Security;
using Swashbuckle.AspNetCore.Filters;

namespace iiwi.AppWire.Configurations;

/// <summary>API Documentation</summary>
public static class ApiDocuments
{
    /// <summary>Adds the open API documents.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddOpenApiDocuments(this IServiceCollection services)
    {
        services.AddOpenApiDocument(options =>
        {
            options.PostProcess = document =>
            {
                document.Info = new NSwag.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Harmony",
                    Description = "IIWI is a mobile-first platform designed to bridge students and counselors through personalized counseling sessions and a collaborative blogging community. Students can discover verified counselors, book sessions, and engage with user-generated content, while counselors showcase their expertise via blogs and manage their professional profiles.",
                    TermsOfService = "https://example.com/terms",
                    Contact = new NSwag.OpenApiContact
                    {
                        Name = "Sajid Khan",
                        Email = "mysipi@outlook.com",
                        Url = "https://www.linkedin.com/in/im-sajid-khan/"
                    },
                    License = new NSwag.OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = "https://github.com/git/git-scm.com/blob/main/MIT-LICENSE.txt"
                    }
                };
            };
            options.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
            {
                In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = NSwag.OpenApiSecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,

            });
            options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
        });

        services.AddEndpointsApiExplorer();

        return services;
    }

    /// <summary>Adds the swagger documents.</summary>
    /// <param name="services">The services.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public static IServiceCollection AddSwaggerDocuments(this IServiceCollection services)
    {
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
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                         Array.Empty<string>()
                    }
                });
            //options.ExampleFilters();

            //NOTE: To enable XML comments, uncomment the following lines

            //var filePath = Path.Combine(AppContext.BaseDirectory + "iiwi.AppWire.xml");
            //options.IncludeXmlComments(filePath);
        });
        services.AddSwaggerExamplesFromAssemblyOf<Program>();

        return services;
    }
}
