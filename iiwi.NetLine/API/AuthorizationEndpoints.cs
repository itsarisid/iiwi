using iiwi.Application.Authentication;
using iiwi.Application.Authorization;
using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

/// <summary>
/// Provides endpoints for role-based authorization management
/// </summary>
/// <remarks>
/// This module handles all role and claim management operations including:
/// - Role CRUD operations
/// - Role-claim associations
/// - User-role assignments
/// All endpoints require authorization.
/// </remarks>
public class AuthorizationEndpoints : IEndpoint
{
    /// <summary>
    /// Registers all authorization-related endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        // Create an Authorization route group for all endpoints
        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(AuthorizationDoc.Group)
            .RequireAuthorization()
            .AddEndpointFilter<ExceptionHandlingFilter>();

        /// <summary>
        /// [GET] /roles - Retrieves all application roles
        /// </summary>
        /// <remarks>
        /// Returns a complete list of all defined roles in the system.
        /// Requires authorization.
        /// </remarks>
        /// <param name="mediator">The Mediator service for handling the request</param>
        /// <returns>List of role definitions</returns>
        /// <response code="200">Returns the list of roles</response>
        /// <response code="401">If user is not authenticated</response>
        /// <response code="403">If user lacks required permissions</response>
        routeGroup.MapVersionedEndpoint(new Configure<RoleRequest, RoleResponse>
        {
            EndpointDetails = AuthorizationDoc.AllRoles,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.AllRoles],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [GET] /roles/{id} - Retrieves a specific role by ID
        /// </summary>
        /// <remarks>
        /// Returns detailed information about a single role.
        /// Requires authorization.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="request">Request containing the role ID</param>
        /// <returns>Role details</returns>
        /// <response code="200">Returns the requested role</response>
        /// <response code="404">If role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<GetByIdRoleRequest, GetByIdRoleResponse>
        {
            EndpointDetails = AuthorizationDoc.RolesById,
            HttpMethod = HttpVerb.Get,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.RolesById],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [POST] /roles - Creates a new role
        /// </summary>
        /// <remarks>
        /// Creates a new role with the specified details.
        /// Requires elevated permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="request">Role creation details</param>
        /// <returns>Operation result</returns>
        /// <response code="201">Role created successfully</response>
        /// <response code="400">If request is invalid</response>
        /// <response code="401">If user is not authenticated</response>
        /// <response code="403">If user lacks required permissions</response>
        routeGroup.MapVersionedEndpoint(new Configure<AddRoleRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.AddRole,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.AddRole],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [PUT] /roles/{id} - Updates an existing role
        /// </summary>
        /// <remarks>
        /// Updates the specified role with new information.
        /// Requires role management permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="id">Role ID to update</param>
        /// <param name="request">Updated role details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Role updated successfully</response>
        /// <response code="404">If role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<UpdateRoleParams, UpdateRoleRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.UpdateRole,
            HttpMethod = HttpVerb.Put,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.UpdateRole],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [DELETE] /roles/{id} - Deletes a role
        /// </summary>
        /// <remarks>
        /// Permanently removes the specified role from the system.
        /// Requires elevated permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="request">Role deletion request</param>
        /// <returns>Operation result</returns>
        /// <response code="204">Role deleted successfully</response>
        /// <response code="404">If role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<DeleteRoleParams, DeleteRoleRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.DeleteRole,
            HttpMethod = HttpVerb.Delete,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.DeleteRole],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [POST] /roles/{roleId}/claims - Adds a claim to a role
        /// </summary>
        /// <remarks>
        /// Associates a new permission claim with the specified role.
        /// Requires role management permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="roleId">Target role ID</param>
        /// <param name="request">Claim details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Claim added successfully</response>
        /// <response code="404">If role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<AddClaimParams, AddClaimRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.AddRoleClaim,
            HttpMethod = HttpVerb.Post,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.AddRoleClaim],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [DELETE] /roles/{roleId}/claims/{id} - Removes a claim from a role
        /// </summary>
        /// <remarks>
        /// Disassociates a claim from the specified role.
        /// Requires role management permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="roleId">Target role ID</param>
        /// <param name="id">Claim ID to remove</param>
        /// <returns>Operation result</returns>
        /// <response code="204">Claim removed successfully</response>
        /// <response code="404">If role or claim is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<RemoveClaimParams, RemoveClaimRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.RemoveRoleClaim,
            HttpMethod = HttpVerb.Delete,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.RemoveRoleClaim],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [GET] /roles/{roleId}/claims - Gets all claims for a role
        /// </summary>
        /// <remarks>
        /// Retrieves all permission claims associated with the specified role.
        /// Requires view permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="roleId">Target role ID</param>
        /// <returns>List of role claims</returns>
        /// <response code="200">Returns the list of claims</response>
        /// <response code="404">If role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<GetRoleClaimsParams, GetRoleClaimsRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.GetRoleClaims,
            HttpMethod = HttpVerb.Get,
            HasUrlParameters = true,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.GetRoleClaims],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [POST] /users/roles - Assigns a role to a user
        /// </summary>
        /// <remarks>
        /// Grants the specified role to a user account.
        /// Requires user management permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="request">User-role assignment details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Role assigned successfully</response>
        /// <response code="404">If user or role is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<AssignRoleToUserRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.AssignRole,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.AssignRole],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [POST] /users/claims - Adds a claim to a user
        /// </summary>
        /// <remarks>
        /// Directly assigns a permission claim to a user account.
        /// Requires elevated permissions.
        /// </remarks>
        /// <param name="mediator">The Mediator service</param>
        /// <param name="request">User-claim assignment details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Claim added successfully</response>
        /// <response code="404">If user is not found</response>
        /// <response code="401">If user is not authenticated</response>
        routeGroup.MapVersionedEndpoint(new Configure<AddClaimToUserRequest, Response>
        {
            EndpointDetails = AuthorizationDoc.AddUserClaim,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Authorization.AddUserClaim],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}
