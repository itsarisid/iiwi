using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.Model.Endpoints;
using iiwi.Model.PingPong;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

/// <summary>
/// Provides test and diagnostic endpoints for the application
/// </summary>
/// <remarks>
/// This module exposes endpoints that return system information and verify application functionality.
/// It includes both public and authenticated endpoints for different testing scenarios.
/// </remarks>
public class DummiesEndpoints : IEndpoint
{
    /// <summary>
    /// Registers test endpoints with the application's route builder
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure routes</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints parameter is null</exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(DummiesDoc.Group)
            .AddEndpointFilter<ExceptionHandlingFilter>();

        /// <summary>
        /// [GET] /test - Public system information endpoint
        /// </summary>
        /// <remarks>
        /// Returns detailed system and application information including:
        /// - Version information
        /// - Runtime environment details
        /// - System metadata
        /// 
        /// This endpoint is publicly accessible and provides basic health check functionality.
        /// </remarks>
        /// <param name="serviceProvider">The application service provider</param>
        /// <returns>System information response</returns>
        /// <response code="200">Returns system information JSON object</response>
        /// <response code="500">If server encounters an error</response>
        routeGroup.MapVersionedEndpoint(new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = DummiesDoc.TestEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                CachePolicy = CachePolicy.NoCache,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });

        /// <summary>
        /// [GET] /authtest - Authenticated system information endpoint
        /// </summary>
        /// <remarks>
        /// Returns enhanced system information including:
        /// - Version information (v2.0)
        /// - Runtime environment details
        /// - System metadata
        /// 
        /// This endpoint requires authentication and serves to:
        /// 1. Verify authorization infrastructure
        /// 2. Provide additional diagnostics for authenticated users
        /// 
        /// Required permissions: Test.Read
        /// </remarks>
        /// <param name="serviceProvider">The application service provider</param>
        /// <returns>Enhanced system information response</returns>
        /// <response code="200">Returns system information JSON object</response>
        /// <response code="401">If user is not authenticated</response>
        /// <response code="403">If user lacks required permissions</response>
        /// <response code="500">If server encounters an error</response>
        routeGroup.MapVersionedEndpoint(new Configure<EmptyRequest, SystemInfoResponse>
            {
                EndpointDetails = DummiesDoc.AuthTestEndpoint,
                HttpMethod = HttpVerb.Get,
                EnableCaching = true,
                AuthorizationPolicies = [Permissions.Test.Read],
                CachePolicy = CachePolicy.DefaultPolicy,
                EnableHttpLogging = true,
                EndpointFilters = ["LoggingFilter"]
            });
    }
}
