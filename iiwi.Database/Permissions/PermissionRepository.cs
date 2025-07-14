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
        Name = user.CodeName,
    };

    public Task<bool> HasPermissionAsync(int userId, string permissionName)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }
    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await context.Permission.FindAsync(id);
    }

    public async Task<Permission> GetPermissionByNameAsync(string name)
    {
        return await context.Permission
            .FirstOrDefaultAsync(p => p.CodeName == name);
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
    public Task<List<Permission>> GetUserPermissionsAsync(int userId)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }
}
