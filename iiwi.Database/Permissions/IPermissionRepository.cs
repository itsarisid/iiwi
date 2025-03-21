
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
}
