using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace iiwi.NetLine.Config;

/// <summary>
/// Provides extension methods for configuring API documentation using Swagger/OpenAPI
/// </summary>
/// <remarks>
/// This configuration sets up:
/// - API versioning and metadata
/// - JWT Bearer authentication documentation
/// - Interactive API documentation UI
/// - Security scheme definitions
/// </remarks>
public static class ApiDocuments
{
    /// <summary>
    /// Configures Swagger/OpenAPI documentation for the API
    /// </summary>
    /// <param name="services">The service collection to configure</param>
    /// <returns>The configured service collection for method chaining</returns>
    /// <remarks>
    /// <para>
    /// This method performs the following configurations:
    /// 1. Sets up basic API information including:
    ///    - Version (v1)
    ///    - Title (IIWI)
    ///    - Detailed description of the platform
    ///    - Contact information
    ///    - License information
    /// </para>
    /// <para>
    /// 2. Configures JWT Bearer authentication documentation:
    ///    - Defines the security scheme
    ///    - Specifies the authorization header format
    ///    - Sets up security requirements
    /// </para>
    /// <para>
    /// 3. Enables the Swagger UI with:
    ///    - Authentication capabilities
    ///    - API versioning support
    ///    - Interactive documentation
    /// </para>
    /// <para>
    /// Usage Example:
    /// <code>
    /// var builder = WebApplication.CreateBuilder(args);
    /// builder.Services.AddApiDocuments();
    /// </code>
    /// </para>
    /// </remarks>
    public static IServiceCollection AddApiDocuments(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // Configure OpenAPI/Swagger services
        services.AddOpenApi();
        services.AddSwaggerGen(options =>
        {
            // API metadata and versioning
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

            // JWT Bearer authentication configuration
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Standard Authorization header using the Bearer scheme (JWT). Example: 'Bearer {token}'",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });

            // Global security requirements
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}