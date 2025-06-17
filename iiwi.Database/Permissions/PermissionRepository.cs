using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using iiwi.Model.Permission;
using System.Linq.Expressions;

namespace iiwi.Database.Permissions;

public class PermissionRepository(ApplicationDbContext context) : EFRepository<Permission>(context), IPermissionRepository
{
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.Name,
    };

    public Task DeleteModelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<PermissionModel> GetModelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Grid<PermissionModel>> GridAsync(GridParameters parameters)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PermissionModel>> ListModelAsync()
    {
        throw new NotImplementedException();
    }
}
