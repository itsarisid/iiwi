using iiwi.Common.Privileges;
using iiwi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iiwi.Data.Seeds;

public static class PermissionSeeder
{
    private static readonly DateTime SeedDate = new(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc);
    public static IEnumerable<Permission> SeedData =>
        [.. Permissions.GetAll()
            .Select((permission, index) => new Permission
            {
                Id = index + 1,
                Description = permission,
                CodeName = permission.ToUpperInvariant(),
                CreationDate = SeedDate  ,
                IsActive = true
            })];

    public static void SeedPermissions(this ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Permission>().HasData(SeedData);
    }

    public static EntityTypeBuilder<Permission> SeedPermissions(this EntityTypeBuilder<Permission> builder)
    {
        builder.HasData(SeedData);
        return builder;
    }

}