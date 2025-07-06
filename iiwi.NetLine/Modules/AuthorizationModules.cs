
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
        routeGroup.MapPost(Authorization.AllRoles.Endpoint,
          IResult (IMediator mediator, RoleRequest request) => mediator
         .HandleAsync<RoleRequest, RoleResponse>(request)
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

        // Get role permissions
        routeGroup.MapGet(Authorization.Permissions.Endpoint,
         IResult (IMediator mediator, [AsParameters] PermissionRequest request) => mediator
        .HandleAsync<PermissionRequest, PermissionResponse>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.Permissions);

        // Update role permissions
        routeGroup.MapPut(Authorization.Permissions.Endpoint,
         IResult (IMediator mediator, [AsParameters] string id, UpdatePermissionRequest request) => mediator
        .HandleAsync<UpdatePermissionRequest, Response>(new UpdatePermissionRequest
        {
            Id = id,
            Permissions = request.Permissions
        })
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Authorization.Permissions);
    }

}