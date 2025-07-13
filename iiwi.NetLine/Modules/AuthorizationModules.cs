
using iiwi.Application;
using iiwi.Application.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

public class AuthorizationModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Authorization.Group)
            .RequireAuthorization();

        // Get all roles
        routeGroup.MapGet(Authorization.AllRoles.Endpoint,
          IResult (IMediator mediator) => mediator
         .HandleAsync<RoleRequest, RoleResponse>(new RoleRequest())
         .Response())
         .WithMappingBehaviour<Response>()
         .WithDocumentation(Authorization.AllRoles);

        // Get role by id
        routeGroup.MapGet(Authorization.RolesById.Endpoint,
         IResult (IMediator mediator, [AsParameters] GetByIdRoleRequest request) => mediator
        .HandleAsync<GetByIdRoleRequest, GetByIdRoleResponse>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.RolesById);

        // Add a new role
        routeGroup.MapPost(Authorization.AddRole.Endpoint,
         IResult (IMediator mediator, [FromBody] AddRoleRequest request) => mediator
        .HandleAsync<AddRoleRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.AddRole);

        // Update an existing role
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

        // Delete a role
        routeGroup.MapDelete(Authorization.DeleteRole.Endpoint,
        IResult (IMediator mediator, [AsParameters] DeleteRoleRequest request) => mediator
        .HandleAsync<DeleteRoleRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.DeleteRole);

        // Add claim to role
        routeGroup.MapPost(Authorization.AddRoleClaim.Endpoint,
        IResult (IMediator mediator, [AsParameters] int roleId, AddClaimRequest request) => mediator
        .HandleAsync<AddClaimRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.AddClaim);

        // Remove claim from role
        routeGroup.MapDelete(Authorization.RemoveRoleClaim.Endpoint,
        IResult (IMediator mediator, [AsParameters] int roleId, [AsParameters] int id) => mediator
        .HandleAsync<RemoveClaimRequest, Response>(new RemoveClaimRequest())
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.RemoveRoleClaim);

        // Get role claims
        routeGroup.MapGet(Authorization.GetRoleClaims.Endpoint,
        IResult (IMediator mediator, [AsParameters] int roleId) => mediator
        .HandleAsync<GetRoleClaimsRequest, Response>(new GetRoleClaimsRequest())
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.GetRoleClaims);

        // Assign role to user
        routeGroup.MapPost(Authorization.AssignRole.Endpoint,
        IResult (IMediator mediator, [FromBody] AssignRoleToUserRequest request) => mediator
        .HandleAsync<AssignRoleToUserRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.AssignRole);

        // Add claim to user
        routeGroup.MapPost(Authorization.AddUserClaim.Endpoint,
        IResult (IMediator mediator, [FromBody] AssignRoleToUserRequest request) => mediator
        .HandleAsync<AssignRoleToUserRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.AddUserClaim);
    }

}