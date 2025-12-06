using iiwi.Common.Privileges;
using iiwi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iiwi.Data.Seeds;

/// <summary>
/// Seeder for permissions.
/// </summary>
public static class PermissionSeeder
{
    private static readonly DateTime SeedDate = new(2025, 5, 9, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Gets the seed data.
    /// </summary>
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

    /// <summary>
    /// Seeds the permissions.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void SeedPermissions(this ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Permission>().HasData(SeedData);
    }

    /// <summary>
    /// Seeds the permissions.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    /// <returns>The entity type builder.</returns>
    public static EntityTypeBuilder<Permission> SeedPermissions(this EntityTypeBuilder<Permission> builder)
    {
        builder.HasData(SeedData);
        return builder;
    }

}