
using DotNetCore.Objects;
using DotNetCore.Repositories;
using iiwi.Domain;
using iiwi.Model.Permission;

namespace iiwi.Database.Permissions;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<Permission> GetPermissionByIdAsync(int id);
    Task<Permission> GetPermissionByNameAsync(string name);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task CreatePermissionAsync(Permission permission);
    Task UpdatePermissionAsync(Permission permission);
    Task DeletePermissionAsync(int id);
    Task<bool> PermissionExistsAsync(int id);

    Task<bool> HasPermissionAsync(int userId, string permissionName);
 
    Task<List<Permission>> GetUserPermissionsAsync(int userId);
}
