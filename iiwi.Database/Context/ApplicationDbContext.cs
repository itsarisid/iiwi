using DotNetCore.Domain;
using iiwi.Common;
using iiwi.Domain;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace iiwi.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, 
    ApplicationRole, int,
    ApplicationUserClaim, 
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>(options)
{
    public DbSet<Permission> Permission { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(General.IdentitySchemaName);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "User");
            entity.HasMany(u => u.UserRoles)            
                  .WithOne(ur => ur.User)              
                  .HasForeignKey(ur => ur.UserId)     
                  .OnDelete(DeleteBehavior.Cascade);   
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable(name: "Role");
            entity.HasMany(r => r.UserRoles)        
                  .WithOne(ur => ur.Role)          
                  .HasForeignKey(ur => ur.RoleId) 
                  .OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<ApplicationUserRole>(entity =>
        {
            entity.ToTable("UserRole");
            //in case you changed the TKey type
            entity.HasKey(key => key.Id);
            entity.HasIndex(ur => ur.UserId);
            entity.HasIndex(ur => ur.RoleId);
        });

        builder.Entity<ApplicationUserClaim>(entity =>
        {
            entity.ToTable("Claims");
        });

        builder.Entity<ApplicationUserLogin>(entity =>
        {
            entity.ToTable("Logins");
            //in case you changed the TKey type
            //entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
        });

        builder.Entity<ApplicationRoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");

        });

        builder.Entity<ApplicationUserToken>(entity =>
        {
            entity.ToTable("UserTokens");
            //in case you changed the TKey type
            //entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

        });

        builder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");
            entity.HasMany(r => r.RolePermissions)        
                  .WithOne(o => o.Permission)           
                  .HasForeignKey(ur => ur.PermissionId)   
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermission");

            entity.HasIndex(e => e.PermissionId, "IX_RolePermission_PermissionId");

            entity.HasIndex(e => e.RoleId, "IX_RolePermission_RoleId");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
        });
    }
}

