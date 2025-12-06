using iiwi.Common;
using iiwi.Data.Seeds;
using iiwi.Domain;
using iiwi.Domain.Identity;
using iiwi.Domain.Logs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iiwi.Database;

/// <summary>
/// The application database context.
/// </summary>
/// <param name="options">The database context options.</param>
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, 
    ApplicationRole, int,
    ApplicationUserClaim, 
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken>(options)
{
    /// <summary>
    /// Gets or sets the permissions.
    /// </summary>
    public DbSet<Permission> Permission { get; set; }

    // NOTE: These logs should move to different DbContext 
    /// <summary>
    /// Gets or sets the audit logs.
    /// </summary>
    public DbSet<AuditLog> AuditLog { get; set; }

    /// <summary>
    /// Configures the database context.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine);


    /// <summary>
    /// Configures the model.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(General.Schema.Identity);

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
            entity.SeedPermissions();
        });
        

        builder.Entity<AuditLog>(entity =>
        {
            // Configure the table name and schema
            entity.ToTable("Audit", General.Schema.Log); // "auditing" is the custom schema name

            // Optional: Add additional configuration for columns if needed
            entity.Property(e => e.ChangedData)
                .HasColumnName("ChangedDataJson")  // Custom column name
                .HasColumnType("nvarchar(max)");      // Explicit data type

            entity.Property(e => e.EntityType)
                .HasMaxLength(100);                // Add length constraint

            entity.Property(e => e.EntityName)
                .HasMaxLength(128);

            entity.Property(e => e.ActionType)
                .HasMaxLength(20);
            
            entity.Property(e => e.RecordId);

            entity.Property(e => e.PerformedBy)
                .HasMaxLength(100);

            entity.Property(e => e.Timestamp)
                .HasColumnName("EventTimestamp");  // Custom column name
        });
    }
}

