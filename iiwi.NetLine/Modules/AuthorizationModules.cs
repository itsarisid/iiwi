
using iiwi.Domain.Identity;
using Microsoft.AspNet.Identity;

namespace iiwi.NetLine.Modules;

public class AuthorizationModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var group = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Authorization.Group)
            .RequireAuthorization();

        // Get all roles
        group.MapGet("/", async (RoleManager<ApplicationRole,int> roleManager) =>
        {
            var roles = await roleManager.Roles
                .Select(r => new RoleDto(r.Id, r.Name, r.Description()))
                .ToListAsync();

            return Results.Ok(roles);
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Get role by ID
        group.MapGet("/{id}", async (string id, RoleManager<IdentityRole> roleManager) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            return role is null
                ? Results.NotFound()
                : Results.Ok(new RoleDto(role.Id, role.Name, role.Description()));
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Create new role
        group.MapPost("/", async (CreateRoleDto dto, RoleManager<IdentityRole> roleManager) =>
        {
            var role = new IdentityRole(dto.Name)
            {
                Description = dto.Description
            };

            var result = await roleManager.CreateAsync(role);

            return result.Succeeded
                ? Results.Created($"/api/roles/{role.Id}", new RoleDto(role.Id, role.Name, role.Description()))
                : Results.BadRequest(result.Errors);
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Update role
        group.MapPut("/{id}", async (string id, UpdateRoleDto dto, RoleManager<IdentityRole> roleManager) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return Results.NotFound();

            role.Name = dto.Name;
            role.Description = dto.Description;

            var result = await roleManager.UpdateAsync(role);

            return result.Succeeded
                ? Results.Ok(new RoleDto(role.Id, role.Name, role.Description()))
                : Results.BadRequest(result.Errors);
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Delete role
        group.MapDelete("/{id}", async (string id, RoleManager<IdentityRole> roleManager) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return Results.NotFound();

            var result = await roleManager.DeleteAsync(role);

            return result.Succeeded
                ? Results.NoContent()
                : Results.BadRequest(result.Errors);
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Get role permissions
        group.MapGet("/{id}/permissions", async (
            string id,
            RoleManager<IdentityRole> roleManager,
            IPermissionService permissionService) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return Results.NotFound();

            var permissions = await permissionService.GetPermissionsForRoleAsync(role.Name);
            return Results.Ok(new RolePermissionsDto(role.Id, permissions.ToList()));
        }).RequireAuthorization(PolicyNames.AdminPolicy);

        // Update role permissions
        group.MapPut("/{id}/permissions", async (
            string id,
            List<string> permissions,
            RoleManager<IdentityRole> roleManager,
            IPermissionService permissionService) =>
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null) return Results.NotFound();

            var result = await permissionService.UpdatePermissionsForRoleAsync(role.Name, permissions);

            return result.Succeeded
                ? Results.Ok()
                : Results.BadRequest(result.Errors);
        }).RequireAuthorization(PolicyNames.AdminPolicy);
    }

    // Extension method to get description from claims
    //private static string? Description(this IdentityRole role)
    //{
    //    return role.Claims.FirstOrDefault(c => c.ClaimType == "Description")?.ClaimValue;
    //}

}
public record RoleDto(string Id, string Name, string? Description);
public record CreateRoleDto(string Name, string? Description);
public record UpdateRoleDto(string Name, string? Description);
public record RolePermissionsDto(string RoleId, List<string> Permissions);