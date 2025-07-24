using iiwi.Application;
using iiwi.Application.Authorization;
using iiwi.NetLine.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

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
public class AuthorizationModules : IEndpoints
{
    /// <summary>
    /// Registers all authorization-related endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Create an authorized route group for all endpoints
        var routeGroup = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Authorization.Group)
            .RequireAuthorization();

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
        routeGroup.MapGet(Authorization.AllRoles.Endpoint,
            IResult (IMediator mediator) => mediator
           .HandleAsync<RoleRequest, RoleResponse>(new RoleRequest())
           .Response())
           .WithMappingBehaviour<Response>()
           .WithDocumentation(Authorization.AllRoles);

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
        routeGroup.MapGet(Authorization.RolesById.Endpoint,
            IResult (IMediator mediator, [AsParameters] GetByIdRoleRequest request) => mediator
            .HandleAsync<GetByIdRoleRequest, GetByIdRoleResponse>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.RolesById);

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
        routeGroup.MapPost(Authorization.AddRole.Endpoint,
            IResult (IMediator mediator, [FromBody] AddRoleRequest request) => mediator
            .HandleAsync<AddRoleRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.AddRole);

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
        routeGroup.MapPut(Authorization.UpdateRole.Endpoint,
            IResult (IMediator mediator, [AsParameters] int id, UpdateRoleRequest request) => mediator
            .HandleAsync<UpdateRoleRequest, Response>(new UpdateRoleRequest
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
            })
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.UpdateRole);

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
        routeGroup.MapDelete(Authorization.DeleteRole.Endpoint,
            IResult (IMediator mediator, [AsParameters] DeleteRoleRequest request) => mediator
            .HandleAsync<DeleteRoleRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.DeleteRole);

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
        routeGroup.MapPost(Authorization.AddRoleClaim.Endpoint,
            IResult (IMediator mediator, [AsParameters] int roleId, AddClaimRequest request) => mediator
            .HandleAsync<AddClaimRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.AddClaim);

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
        routeGroup.MapDelete(Authorization.RemoveRoleClaim.Endpoint,
            IResult (IMediator mediator, [AsParameters] int roleId, [AsParameters] int id) => mediator
            .HandleAsync<RemoveClaimRequest, Response>(new RemoveClaimRequest())
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.RemoveRoleClaim);

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
        routeGroup.MapGet(Authorization.GetRoleClaims.Endpoint,
            IResult (IMediator mediator, [AsParameters] int roleId) => mediator
            .HandleAsync<GetRoleClaimsRequest, Response>(new GetRoleClaimsRequest())
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.GetRoleClaims);

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
        routeGroup.MapPost(Authorization.AssignRole.Endpoint,
            IResult (IMediator mediator, [FromBody] AssignRoleToUserRequest request) => mediator
            .HandleAsync<AssignRoleToUserRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.AssignRole);

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
        routeGroup.MapPost(Authorization.AddUserClaim.Endpoint,
            IResult (IMediator mediator, [FromBody] AssignRoleToUserRequest request) => mediator
            .HandleAsync<AssignRoleToUserRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Authorization.AddUserClaim);
    }
}