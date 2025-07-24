using iiwi.Model;

namespace iiwi.NetLine.Extensions;

/// <summary>
/// Provides extension methods for configuring API endpoint groups with standardized metadata
/// </summary>
/// <remarks>
/// These extensions help maintain consistent API documentation and organization by:
/// - Applying standardized OpenAPI/Swagger metadata
/// - Ensuring consistent grouping and tagging
/// - Centralizing endpoint documentation configuration
/// </remarks>
public static class EndpointGroupExtensions
{
    /// <summary>
    /// Configures a route group with standardized endpoint metadata
    /// </summary>
    /// <param name="builder">The RouteGroupBuilder to configure</param>
    /// <param name="group">EndpointDetails containing the metadata to apply</param>
    /// <returns>The configured RouteGroupBuilder for method chaining</returns>
    /// <remarks>
    /// <para>
    /// Applies the following OpenAPI/Swagger metadata:
    /// - Tags for logical grouping in documentation
    /// - Name for endpoint identification
    /// - Description for API documentation
    /// - OpenAPI inclusion
    /// </para>
    /// <para>
    /// Usage Example:
    /// <code>
    /// app.MapGroup("/api/users")
    ///    .WithGroup(UsersEndpoints.Group)
    ///    .MapGet("/", GetUsers);
    /// </code>
    /// </para>
    /// <para>
    /// This ensures all endpoints in a group share consistent documentation
    /// and organizational structure in generated API documentation.
    /// </para>
    /// </remarks>
    public static RouteGroupBuilder WithGroup(this RouteGroupBuilder builder, EndpointDetails group)
    {
        return builder
            .WithTags(group.Tags)       // For Swagger/OpenAPI grouping
            .WithName(group.Name)       // For endpoint identification
            .WithDescription(group.Description)  // For API documentation
            .WithOpenApi();             // Include in OpenAPI spec
    }
}