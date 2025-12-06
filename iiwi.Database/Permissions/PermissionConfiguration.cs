
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using iiwi.Domain;
using iiwi.Common;

namespace iiwi.Database;

/// <summary>
/// Configuration for the Permission entity.
/// </summary>
public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    /// <summary>
    /// Configures the Permission entity.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(nameof(Permission), General.Schema.Name);
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .UseHiLo(General.DbSequenceName);
        builder.Property(entity => entity.CodeName).HasMaxLength(250).IsRequired();
        builder.Property(entity => entity.Description).HasMaxLength(450).IsRequired();
        builder.Property(entity => entity.IsActive).IsRequired();

        builder.Property(entity => entity.IsDeleted).ValueGeneratedOnAddOrUpdate();
        builder.Property(entity => entity.DeletedDate);
        //builder.Property(entity => entity.DeletedByUserId);

        builder.Property(entity => entity.CreationDate).ValueGeneratedOnAdd().IsRequired();
        //builder.Property(entity => entity.CreatedByUserId).IsRequired();

        builder.Property(entity => entity.UpdateDate).ValueGeneratedOnUpdate();
        //builder.Property(entity => entity.UpdateByUserId);
    }
}
