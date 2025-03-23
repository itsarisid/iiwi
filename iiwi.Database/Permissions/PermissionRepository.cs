using DotNetCore.Objects;
using DotNetCore.Repositories;
using iiwi.Domain;
using iiwi.Model.Permission;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iiwi.Database.Permissions;

public class PermissionRepository(iiwiDbContext context) : Repository<Permission>(context), IPermissionRepository
{
    public static Expression<Func<Permission, PermissionModel>> Model => user => new PermissionModel
    {
        Id = user.Id,
        Name = user.Name,
    };


    public Task<PermissionModel> GetModelAsync(long id) => Queryable.Where(user => user.Id == id).Select(Model).SingleOrDefaultAsync();

    public Task<Grid<PermissionModel>> GridAsync(GridParameters parameters) => Queryable.Select(Model).GridAsync(parameters);

    public async Task<IEnumerable<PermissionModel>> ListModelAsync() => await Queryable.Select(Model).ToListAsync();
}
