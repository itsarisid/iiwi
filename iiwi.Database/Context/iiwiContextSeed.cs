using iiwi.Domain;
using Microsoft.EntityFrameworkCore;

namespace iiwi.Database;

public static class iiwiContextSeed
{
    public static void Seed(this ModelBuilder builder) => builder.SeedData();
    private static void SeedData(this ModelBuilder builder)
    {
        builder.Entity<Permission>(entity => entity.HasData(new Permission
        {
            Id = 1L,
            Name = "Admin",
            CodeName= "admin",
            IsActive = true,
            CreatedByUserId = new Guid(),
        }));
    }
}
