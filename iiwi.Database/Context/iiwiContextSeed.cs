using iiwi.Domain;
using Microsoft.EntityFrameworkCore;

namespace iiwi.Database;

/// <summary>
/// Seed data for the context.
/// </summary>
public static class iiwiContextSeed
{
    /// <summary>
    /// Seeds the database.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    public static void Seed(this ModelBuilder builder) => builder.SeedData();
    private static void SeedData(this ModelBuilder builder)
    {
        builder.Entity<Permission>(entity => entity.HasData(new Permission
        {
            Id = 1L,
            CodeName = "Admin",
            IsActive = true,
            IsDeleted = false,
            //CreatedByUserId = new Guid("00000000-0000-0000-0000-000000000000"),
            CreationDate = new DateTime(2025, 3, 23),
            //UpdateByUserId = new Guid("00000000-0000-0000-0000-000000000000"),
            UpdateDate = new DateTime(2025, 3, 23),
        }));
    }
}
