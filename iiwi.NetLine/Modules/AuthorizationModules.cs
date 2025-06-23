
using iiwi.Application;
using iiwi.Application.Authorization;

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

        routeGroup.MapGet("/{id}", async (string id, RoleManager<IdentityRole> roleManager) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            return role is null
                ? Results.NotFound()
                : Results.Ok(new RoleDto(role.Id, role.Name, role.Description()));
        }).RequireAuthorization(PolicyNames.AdminPolicy);


    }

}
public record RoleDto(string Id, string Name, string? Description);
public record CreateRoleDto(string Name, string? Description);
public record UpdateRoleDto(string Name, string? Description);
public record RolePermissionsDto(string RoleId, List<string> Permissions);