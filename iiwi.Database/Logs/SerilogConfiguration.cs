using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using iiwi.Domain.Logs;

namespace iiwi.Database.Logs;

/// <summary>
/// Configuration for the Serilog entity.
/// </summary>
public sealed class SerilogConfiguration : IEntityTypeConfiguration<Serilog>
{
    /// <summary>
    /// Configures the Serilog entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Serilog> builder)
    {
        builder.ToTable(nameof(Serilog), "Log");
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Message).HasMaxLength(-1);
        builder.Property(entity => entity.MessageTemplate).HasMaxLength(-1);
        builder.Property(entity => entity.Level).HasMaxLength(-1);
        builder.Property(entity => entity.TimeStamp);
        builder.Property(entity => entity.Exception).HasMaxLength(-1);
        builder.Property(entity => entity.Properties).HasMaxLength(-1);
    }
}