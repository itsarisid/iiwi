using DotNetCore.Objects;
using DotNetCore.Repositories;
using iiwi.Domain;
using iiwi.Model.Permission;
namespace iiwi.Database.Permissions
{
    internal interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task<RolePermission> GetModelAsync(long id);

        Task<Grid<RolePermission>> GridAsync(GridParameters parameters);

        Task<IEnumerable<RolePermission>> ListModelAsync();

        Task DeleteModelAsync(long id);
    }
}
