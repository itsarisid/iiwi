using DotNetCore.Objects;
using DotNetCore.Repositories;
using iiwi.Domain;
using iiwi.Model.Permission;
namespace iiwi.Database.Permissions
{
    internal interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task AddPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
        Task RemoveAllPermissionFromRoleAsync(int roleId);
    }
}
