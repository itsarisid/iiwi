using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using iiwi.Model.Permission;

namespace iiwi.Database.Permissions
{
    public class RolePermissionRepository(ApplicationDbContext context) : EFRepository<Permission>(context), IPermissionRepository
    {
        public Task<PermissionModel> GetModelAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Grid<PermissionModel>> GridAsync(GridParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermissionAsync(int userId, string permissionName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionModel>> ListModelAsync()
        {
            throw new NotImplementedException();
        }
    }
}
