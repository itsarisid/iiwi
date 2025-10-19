using Asp.Versioning;
using iiwi.NetLine.Swagger;
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
        services.ApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.SunsetDays = 90;
            config.PolicyLink = "/api/versioning-policy";
        });
        services.AddSwaggerGen(options =>
        {
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
        services.ConfigureOptions<NamedSwaggerGenOptions>();

        return services;
    }
}