using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using iiwi.Model.Permission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace iiwi.Database.Permissions;

public class PermissionRepository(ApplicationDbContext context) : EFRepository<Permission>(context), IPermissionRepository
{
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.Name,
    };

    public Task DeleteModelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionModel> GetModelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Grid<PermissionModel>> GridAsync(GridParameters parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasPermissionAsync(int userId, string permissionName)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .AnyAsync(rp =>
                rp.Permission.Name == permissionName);
    }

    public Task<IEnumerable<PermissionModel>> ListModelAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Permission>> GetUserPermissionsAsync(int userId)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission)
            .Distinct()
            .ToListAsync();
    }
}
