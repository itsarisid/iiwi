using DotNetCore.Repositories;
using iiwi.Domain;

namespace iiwi.Database.Permissions;

/// <summary>
/// Interface for permission repository.
/// </summary>
public interface IPermissionRepository : IRepository<Permission>
{
    /// <summary>
    /// Gets a permission by ID.
    /// </summary>
    /// <param name="id">The permission ID.</param>
    /// <returns>The permission.</returns>
    Task<Permission> GetPermissionByIdAsync(int id);

    /// <summary>
    /// Gets a permission by name.
    /// </summary>
    /// <param name="name">The permission name.</param>
    /// <returns>The permission.</returns>
    Task<Permission> GetPermissionByNameAsync(string name);

    /// <summary>
    /// Gets all permissions.
    /// </summary>
    /// <returns>A collection of permissions.</returns>
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();

    /// <summary>
    /// Creates a new permission.
    /// </summary>
    /// <param name="permission">The permission to create.</param>
    Task CreatePermissionAsync(Permission permission);

    /// <summary>
    /// Updates a permission.
    /// </summary>
    /// <param name="permission">The permission to update.</param>
    Task UpdatePermissionAsync(Permission permission);

    /// <summary>
    /// Deletes a permission by ID.
    /// </summary>
    /// <param name="id">The permission ID.</param>
    Task DeletePermissionAsync(int id);

    /// <summary>
    /// Checks if a permission exists.
    /// </summary>
    /// <param name="id">The permission ID.</param>
    /// <returns>True if the permission exists, otherwise false.</returns>
    Task<bool> PermissionExistsAsync(int id);

    /// <summary>
    /// Checks if a user has a specific permission.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="permissionName">The permission name.</param>
    /// <returns>True if the user has the permission, otherwise false.</returns>
    Task<bool> HasPermissionAsync(int userId, string permissionName);

    /// <summary>
    /// Gets permissions for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A list of permissions.</returns>
    Task<List<Permission>> GetUserPermissionsAsync(int userId);
}
