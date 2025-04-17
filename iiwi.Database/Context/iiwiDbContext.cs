using iiwi.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace iiwi.Database;

public sealed class iiwiDbContext(DbContextOptions options) : DbContext(options)
{
    //protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(iiwiDbContext).Assembly).Seed();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasSequence(General.DbSequenceName).IncrementsBy(100);

        builder.ApplyConfigurationsFromAssembly(typeof(iiwiDbContext).Assembly);
    }
}
