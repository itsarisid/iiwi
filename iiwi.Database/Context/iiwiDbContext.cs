using iiwi.Common;
using Microsoft.EntityFrameworkCore;


namespace iiwi.Database;

/// <summary>
/// The iiwi database context.
/// </summary>
/// <param name="options">The database context options.</param>
public sealed class iiwiDbContext(DbContextOptions options) : DbContext(options)
{
    //protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(typeof(iiwiDbContext).Assembly).Seed();

    /// <summary>
    /// Configures the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasSequence(General.DbSequenceName).IncrementsBy(100);

        builder.ApplyConfigurationsFromAssembly(typeof(iiwiDbContext).Assembly);
    }
}
