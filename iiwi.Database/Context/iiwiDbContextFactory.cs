using iiwi.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace iiwi.Database.Context;

/// <summary>
/// Factory for creating the iiwi database context.
/// </summary>
public class iiwiDbContextFactory : IDesignTimeDbContextFactory<iiwiDbContext>
{
    /// <summary>
    /// Creates the database context.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns>The iiwi database context.</returns>
    public iiwiDbContext CreateDbContext(string[] args)
    {
        // Get environment
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Build config
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var builder = new DbContextOptionsBuilder<iiwiDbContext>();

        var connectionString = configuration.GetConnectionString(General.DbConnectionName);

        builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("iiwi.Database"));

        //builder.UseSqlServer(connectionString);

        return new iiwiDbContext(builder.Options);
    }
}
