using Microsoft.EntityFrameworkCore;

namespace iiwi.Database;

public sealed class iiwiDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(iiwiDbContext).Assembly).Seed();
}
