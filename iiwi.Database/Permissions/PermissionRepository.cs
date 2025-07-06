using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using iiwi.Model.Permission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Security;
using System.Security.Claims;

namespace iiwi.Database.Permissions;

public class PermissionRepository(ApplicationDbContext context) : EFRepository<Permission>(context), IPermissionRepository
{
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.Name,
    };

    public async Task<bool> HasPermissionAsync(int userId, string permissionName)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .AnyAsync(rp =>
                rp.Permission.Name == permissionName);
    }
    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await context.Permission.FindAsync(id);
    }

    public async Task<Permission> GetPermissionByNameAsync(string name)
    {
        return await context.Permission
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await context.Permission.ToListAsync();
    }

    public async Task CreatePermissionAsync(Permission permission)
    {
        await context.Permission.AddAsync(permission);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePermissionAsync(Permission permission)
    {
        context.Permission.Update(permission);
        await context.SaveChangesAsync();
    }

    public async Task DeletePermissionAsync(int id)
    {
        await context.Permission
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> PermissionExistsAsync(int id)
    {
        return await context.Permission
            .AnyAsync(p => p.Id == id);
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

    public async Task<IEnumerable<RolePermission>> GetPermissionsByRoleIdAsync(int id)
    {
        return await context.RolePermissions
            .Where(rp => rp.RoleId == id)
            .Select(rp => new RolePermission
            {
                Id = rp.Id,
                RoleId = rp.RoleId,
                Permission = new Permission
                {
                    Id = rp.Permission.Id,
                    Name = rp.Permission.Name
                }
            }).ToListAsync();
    }

    public async Task UpdateRolePermissionsForRoleAsync(int roleId, List<int> permissionIds)
    {
        // Remove existing permissions for the role in a single operation
        await context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .ExecuteDeleteAsync();

        // Add new permissions if any were provided
        if (permissionIds.Count > 0)
        {
            var newRolePermissions = permissionIds
                .Select(permissionId => new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                })
                .ToList();

            await context.RolePermissions.AddRangeAsync(newRolePermissions);
        }

        await context.SaveChangesAsync();
    }
}
