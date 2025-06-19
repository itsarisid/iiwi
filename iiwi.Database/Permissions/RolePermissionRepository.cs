using DotNetCore.EntityFrameworkCore;
using DotNetCore.Objects;
using iiwi.Domain;
using Microsoft.EntityFrameworkCore;

namespace iiwi.Database.Permissions
{
    public class RolePermissionRepository(ApplicationDbContext context) : EFRepository<RolePermission>(context), IRolePermissionRepository
    {

        public async Task AddPermissionToRoleAsync(int roleId, int permissionId)
        {
            if (await context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId))
                return;

            context.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = permissionId });
            await context.SaveChangesAsync();
        }

        public async Task RemoveAllPermissionFromRoleAsync(int roleId)
        {
            await context.RolePermissions
                 .Where(rp => rp.RoleId == roleId)
                 .ExecuteDeleteAsync();
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            await context.RolePermissions
                .Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                .ExecuteDeleteAsync();
        }
    }
}
