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

    public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
    {
        if (await context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId))
            return;

        context.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
        await context.SaveChangesAsync();
    }

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
        .Join(context.RolePermissions,
            ur => ur.RoleId,
            rp => rp.RoleId,
            (ur, rp) => new { ur.UserId, rp.Permission })
        .AnyAsync(x => x.UserId == userId && x.Permission.CodeName == permissionName);
    }

    public Task<IEnumerable<PermissionModel>> ListModelAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
    {
        throw new NotImplementedException();
    }
}
