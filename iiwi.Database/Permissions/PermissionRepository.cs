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
    /// <summary>
    /// Gets the Model.
    /// </summary>
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.CodeName,
    };

    /// <summary>
    /// Executes the HasPermissionAsync operation.
    /// </summary>
    public Task<bool> HasPermissionAsync(int userId, string permissionName)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }
    /// <summary>
    /// Executes the GetPermissionByIdAsync operation.
    /// </summary>
    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await context.Permission.FindAsync(id);
    }

    /// <summary>
    /// Executes the GetPermissionByNameAsync operation.
    /// </summary>
    public async Task<Permission> GetPermissionByNameAsync(string name)
    {
        return await context.Permission
            .FirstOrDefaultAsync(p => p.CodeName == name);
    }

    /// <summary>
    /// Executes the GetAllPermissionsAsync operation.
    /// </summary>
    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await context.Permission.ToListAsync();
    }

    /// <summary>
    /// Executes the CreatePermissionAsync operation.
    /// </summary>
    public async Task CreatePermissionAsync(Permission permission)
    {
        await context.Permission.AddAsync(permission);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Executes the UpdatePermissionAsync operation.
    /// </summary>
    public async Task UpdatePermissionAsync(Permission permission)
    {
        context.Permission.Update(permission);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Executes the DeletePermissionAsync operation.
    /// </summary>
    public async Task DeletePermissionAsync(int id)
    {
        await context.Permission
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    /// <summary>
    /// Executes the PermissionExistsAsync operation.
    /// </summary>
    public async Task<bool> PermissionExistsAsync(int id)
    {
        return await context.Permission
            .AnyAsync(p => p.Id == id);
    }
    /// <summary>
    /// Executes the GetUserPermissionsAsync operation.
    /// </summary>
    public Task<List<Permission>> GetUserPermissionsAsync(int userId)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }
}
