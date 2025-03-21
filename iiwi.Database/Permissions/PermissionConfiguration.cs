﻿
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using iiwi.Domain;

namespace iiwi.Database;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(nameof(Permission), Common.SchemaName);
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(entity => entity.Name).HasMaxLength(250).IsRequired();
        builder.Property(entity => entity.IsActive).IsRequired();

        builder.Property(entity => entity.IsDeleted);
        builder.Property(entity => entity.DeletedDate);
        builder.Property(entity => entity.DeletedByUserId);

        builder.Property(entity => entity.CreationDate).ValueGeneratedOnAdd().IsRequired();
        builder.Property(entity => entity.CreatedByUserId).IsRequired();

        builder.Property(entity => entity.UpdateDate).ValueGeneratedOnUpdate();
        builder.Property(entity => entity.UpdateByUserId);
    }
}
