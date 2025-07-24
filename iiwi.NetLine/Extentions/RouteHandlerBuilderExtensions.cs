using iiwi.Model;

namespace iiwi.NetLine.Extensions;

/// <summary>
/// Provides extension methods for enhancing RouteHandlerBuilder functionality
/// </summary>
/// <remarks>
/// These extensions standardize endpoint configuration by:
/// - Adding consistent response type documentation
/// - Centralizing OpenAPI/Swagger documentation
/// - Reducing boilerplate in endpoint definitions
/// </remarks>
public static class RouteHandlerBuilderExtensions
{
    /// <summary>
    /// Configures the endpoint to document its response type
    /// </summary>
    /// <typeparam name="T">The response type to document</typeparam>
    /// <param name="builder">The RouteHandlerBuilder instance</param>
    /// <returns>The original builder for method chaining</returns>
    /// <remarks>
    /// This extension:
    /// - Documents the response type in OpenAPI/Swagger
    /// - Ensures consistent response type documentation
    /// - Supports method chaining for fluent configuration
    /// 
    /// Usage:
    /// <code>
    /// app.MapGet("/items", GetItems)
    ///    .WithMappingBehaviour&lt;ItemResponse&gt;()
    /// </code>
    /// </remarks>
    public static RouteHandlerBuilder WithMappingBehaviour<T>(this RouteHandlerBuilder builder)
    {
        return builder.Produces<T>();
    }

    /// <summary>
    /// Configures comprehensive endpoint documentation
    /// </summary>
    /// <param name="builder">The RouteHandlerBuilder instance</param>
    /// <param name="group">Endpoint metadata containing documentation details</param>
    /// <returns>The original builder for method chaining</returns>
    /// <remarks>
    /// <para>
    /// This extension configures:
    /// - Endpoint name (for URL generation)
    /// - Summary (brief description)
    /// - Detailed description
    /// - OpenAPI inclusion
    /// </para>
    /// <para>
    /// Standardizes documentation across all endpoints using a centralized
    /// metadata source (EndpointDetails).
    /// </para>
    /// <para>
    /// Usage:
    /// <code>
    /// app.MapGet("/items", GetItems)
    ///    .WithDocumentation(ItemsEndpoints.GetItems)
    /// </code>
    /// </para>
    /// </remarks>
    public static RouteHandlerBuilder WithDocumentation(
        this RouteHandlerBuilder builder,
        EndpointDetails group)
    {
        return builder.WithName(group.Name)
                     .WithSummary(group.Summary)
                     .WithDescription(group.Description)
                     .IncludeInOpenApi();
    }
}