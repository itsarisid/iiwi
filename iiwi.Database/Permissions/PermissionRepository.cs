using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using iiwi.Model.Permission;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;


namespace iiwi.Database.Permissions;

/// <summary>
/// Repository for managing permissions.
/// </summary>
/// <param name="context">The application database context.</param>
public class PermissionRepository(ApplicationDbContext context) : EFRepository<Permission>(context), IPermissionRepository
{
    /// <summary>
    /// Expression for projecting permission model.
    /// </summary>
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.CodeName,
    };

    /// <inheritdoc />
    public Task<bool> HasPermissionAsync(int userId, string permissionName)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }

    /// <inheritdoc />
    public async Task<Permission> GetPermissionByIdAsync(int id)
    {
        return await context.Permission.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<Permission> GetPermissionByNameAsync(string name)
    {
        return await context.Permission
            .FirstOrDefaultAsync(p => p.CodeName == name);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await context.Permission.ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreatePermissionAsync(Permission permission)
    {
        await context.Permission.AddAsync(permission);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdatePermissionAsync(Permission permission)
    {
        context.Permission.Update(permission);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeletePermissionAsync(int id)
    {
        await context.Permission
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
    }

    /// <inheritdoc />
    public async Task<bool> PermissionExistsAsync(int id)
    {
        return await context.Permission
            .AnyAsync(p => p.Id == id);
    }

    /// <inheritdoc />
    public Task<List<Permission>> GetUserPermissionsAsync(int userId)
    {
        throw new NotImplementedException("This method is not implemented yet. Please implement it according to your requirements.");
    }
}
