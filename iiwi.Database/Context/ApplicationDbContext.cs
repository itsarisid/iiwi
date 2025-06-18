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
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        builder.Entity<ApplicationUserRole>(entity =>
        {
            entity.ToTable("UserRole");
            //in case you changed the TKey type
            //entity.HasKey(key => new { key.UserId, key.RoleId });
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
            //in case you changed the TKey type
            entity.HasKey(key => new { key.Id });
        });

        builder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("RolePermission");
            //in case you changed the TKey type
            //entity.HasKey(key => new { key.Id });
        });
    }
}

