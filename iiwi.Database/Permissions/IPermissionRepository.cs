
using DotNetCore.Objects;
using DotNetCore.Repositories;
using iiwi.Domain;
using iiwi.Model.Permission;

namespace iiwi.Database.Permissions;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<PermissionModel> GetModelAsync(long id);

    Task<Grid<PermissionModel>> GridAsync(GridParameters parameters);

    Task<IEnumerable<PermissionModel>> ListModelAsync();

    Task<bool> HasPermissionAsync(int userId, string permissionName);
    Task AddPermissionToRoleAsync(int roleId, int permissionId);
    Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
}
