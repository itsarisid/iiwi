using Microsoft.Extensions.Configuration;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;

namespace iiwi.CLI;

public static class Helper
{
    /// <summary>
    /// Creates a command to add a new migration.
    /// </summary>
    /// <summary>
    /// Creates a command named "add" configured to add a new Entity Framework migration.
    /// </summary>
    /// <remarks>
    /// The command requires a migration name argument and provides options for the DbContext, project path (defaults to the current directory), and output directory for migration files.
    /// </remarks>
    /// <returns>The configured Command object.</returns>
    public static Command CreateAddMigrationCommand()
    {
        var command = new Command("add", "Add a new migration");

        var nameArg = new Argument<string>("name", "Migration name");
        var contextOption = new Option<string?>(
            aliases: new[] { "--context", "-c" },
            description: "DbContext to use");
        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var outputDirOption = new Option<string?>(
            aliases: new[] { "--output-dir", "-o" },
            description: "Output directory for migration files");

        command.AddArgument(nameArg);
        command.AddOption(contextOption);
        command.AddOption(projectOption);
        command.AddOption(outputDirOption);

        command.SetHandler(async (name, context, project, outputDir) =>
        {
            await ExecuteMigrationCommand("add", name, context, project, outputDir);
        }, nameArg, contextOption, projectOption, outputDirOption);

        return command;
    }

    /// <summary>
    /// Creates a command to update the database to a specified migration.
    /// </summary>
    /// <summary>
    /// Builds and returns the "update" CLI command configured to update the database to a specified migration or to the latest migration.
    /// </summary>
    /// <returns>The configured Command object.</returns>
    public static Command CreateUpdateDatabaseCommand()
    {
        var command = new Command("update", "Update database to latest or specified migration");

        var migrationArg = new Argument<string?>(
            "migration",
            () => null,
            "Target migration (optional, defaults to latest)");
        var contextOption = new Option<string?>(
            aliases: new[] { "--context", "-c" },
            description: "DbContext to use");
        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var connectionOption = new Option<string?>(
            aliases: new[] { "--connection", "-conn" },
            description: "Connection string (overrides appsettings)");
        var connectionNameOption = new Option<string?>(
            aliases: new[] { "--connection-name", "-cn" },
            description: "Connection string name from appsettings.json",
            getDefaultValue: () => "DefaultConnection");
        var environmentOption = new Option<string?>(
            aliases: new[] { "--environment", "-e" },
            description: "Environment (Development, Production, etc.)",
            getDefaultValue: () => "Development");

        command.AddArgument(migrationArg);
        command.AddOption(contextOption);
        command.AddOption(projectOption);
        command.AddOption(connectionOption);
        command.AddOption(connectionNameOption);
        command.AddOption(environmentOption);

        command.SetHandler(async (migration, context, project, connection, connectionName, environment) =>
        {
            await UpdateDatabase(migration, context, project, connection, connectionName, environment);
        }, migrationArg, contextOption, projectOption, connectionOption, connectionNameOption, environmentOption);

        return command;
    }

    /// <summary>
    /// Creates a command to list all available migrations.
    /// </summary>
    /// <summary>
    /// Builds and returns the "list" CLI command which lists all available Entity Framework migrations and exposes options for DbContext, project path, connection string/name, and environment.
    /// </summary>
    /// <returns>The configured Command object.</returns>
    public static Command CreateListMigrationsCommand()
    {
        var command = new Command("list", "List all migrations");

        var contextOption = new Option<string?>(
            aliases: new[] { "--context", "-c" },
            description: "DbContext to use");
        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var connectionOption = new Option<string?>(
            aliases: new[] { "--connection", "-conn" },
            description: "Connection string (overrides appsettings)");
        var connectionNameOption = new Option<string?>(
            aliases: new[] { "--connection-name", "-cn" },
            description: "Connection string name from appsettings.json");
        var environmentOption = new Option<string?>(
            aliases: new[] { "--environment", "-e" },
            description: "Environment (Development, Production, etc.)");

        command.AddOption(contextOption);
        command.AddOption(projectOption);
        command.AddOption(connectionOption);
        command.AddOption(connectionNameOption);
        command.AddOption(environmentOption);

        command.SetHandler(async (context, project, connection, connectionName, environment) =>
        {
            await ListMigrations(context, project, connection, connectionName, environment);
        }, contextOption, projectOption, connectionOption, connectionNameOption, environmentOption);

        return command;
    }

    /// <summary>
    /// Creates a command to remove the last migration.
    /// </summary>
    /// <summary>
    /// Creates the "remove" CLI command used to remove the last Entity Framework migration.
    /// </summary>
    /// <remarks>
    /// The command accepts options for DbContext (--context / -c), project path (--project / -p, defaults to the current directory),
    /// and a force flag (--force / -f) to remove migrations that have been applied to the database.
    /// </remarks>
    /// <returns>The configured Command object.</returns>
    public static Command CreateRemoveMigrationCommand()
    {
        var command = new Command("remove", "Remove the last migration");

        var contextOption = new Option<string?>(
            aliases: new[] { "--context", "-c" },
            description: "DbContext to use");
        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var forceOption = new Option<bool>(
            aliases: new[] { "--force", "-f" },
            description: "Force remove even if applied to database");

        command.AddOption(contextOption);
        command.AddOption(projectOption);
        command.AddOption(forceOption);

        command.SetHandler(async (context, project, force) =>
        {
            await RemoveMigration(context, project, force);
        }, contextOption, projectOption, forceOption);

        return command;
    }

    /// <summary>
    /// Creates a command to generate a SQL script from migrations.
    /// </summary>
    /// <summary>
    /// Creates and configures the "script" CLI command for generating a SQL script from migrations.
    /// </summary>
    /// <returns>The configured Command for generating migration SQL scripts.</returns>
    public static Command CreateScriptMigrationCommand()
    {
        var command = new Command("script", "Generate SQL script for migrations");

        var fromArg = new Argument<string?>("from", () => null, "Starting migration");
        var toArg = new Argument<string?>("to", () => null, "Ending migration");
        var contextOption = new Option<string?>(
            aliases: new[] { "--context", "-c" },
            description: "DbContext to use");
        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var outputOption = new Option<string?>(
            aliases: new[] { "--output", "-o" },
            description: "Output file path");
        var idempotentOption = new Option<bool>(
            aliases: new[] { "--idempotent", "-i" },
            description: "Generate idempotent script");
        var connectionOption = new Option<string?>(
            aliases: new[] { "--connection", "-conn" },
            description: "Connection string (overrides appsettings)");
        var connectionNameOption = new Option<string?>(
            aliases: new[] { "--connection-name", "-cn" },
            description: "Connection string name from appsettings.json");
        var environmentOption = new Option<string?>(
            aliases: new[] { "--environment", "-e" },
            description: "Environment (Development, Production, etc.)");

        command.AddArgument(fromArg);
        command.AddArgument(toArg);
        command.AddOption(contextOption);
        command.AddOption(projectOption);
        command.AddOption(outputOption);
        command.AddOption(idempotentOption);
        command.AddOption(connectionOption);
        command.AddOption(connectionNameOption);
        command.AddOption(environmentOption);

        command.SetHandler(async (InvocationContext context) =>
        {
            var fromValue = context.ParseResult.GetValueForArgument(fromArg);
            var toValue = context.ParseResult.GetValueForArgument(toArg);
            var contextValue = context.ParseResult.GetValueForOption(contextOption);
            var projectValue = context.ParseResult.GetValueForOption(projectOption);
            var outputValue = context.ParseResult.GetValueForOption(outputOption);
            var idempotentValue = context.ParseResult.GetValueForOption(idempotentOption);
            var connectionValue = context.ParseResult.GetValueForOption(connectionOption);
            var connectionNameValue = context.ParseResult.GetValueForOption(connectionNameOption);
            var environmentValue = context.ParseResult.GetValueForOption(environmentOption);

            await ScriptMigration(
                fromValue,
                toValue,
                contextValue,
                projectValue,
                outputValue,
                idempotentValue,
                connectionValue,
                connectionNameValue,
                environmentValue
            );
        });

        return command;
    }

    /// <summary>
    /// Creates a command to display configuration information.
    /// </summary>
    /// <summary>
    /// Creates the "info" command which displays project, environment, connection name, and connection string information.
    /// </summary>
    /// <returns>The configured Command that, when invoked, displays configuration and connection details.</returns>
    public static Command CreateInfoCommand()
    {
        var command = new Command("info", "Display connection string and configuration information");

        var projectOption = new Option<string?>(
            aliases: new[] { "--project", "-p" },
            description: "Project path",
            getDefaultValue: () => Directory.GetCurrentDirectory());
        var connectionNameOption = new Option<string?>(
            aliases: new[] { "--connection-name", "-cn" },
            description: "Connection string name from appsettings.json",
            getDefaultValue: () => "DefaultConnection");
        var environmentOption = new Option<string?>(
            aliases: new[] { "--environment", "-e" },
            description: "Environment (Development, Production, etc.)",
            getDefaultValue: () => "Development");
        var showConnectionOption = new Option<bool>(
            aliases: new[] { "--show-connection", "-s" },
            description: "Show full connection string (sensitive data)");

        command.AddOption(projectOption);
        command.AddOption(connectionNameOption);
        command.AddOption(environmentOption);
        command.AddOption(showConnectionOption);

        command.SetHandler((project, connectionName, environment, showConnection) =>
        {
            DisplayInfo(project, connectionName, environment, showConnection);
        }, projectOption, connectionNameOption, environmentOption, showConnectionOption);

        return command;
    }

    /// <summary>
    /// Displays configuration and connection string information.
    /// </summary>
    /// <param name="project">The project path.</param>
    /// <param name="connectionName">The name of the connection string.</param>
    /// <param name="environment">The environment name.</param>
    /// <summary>
    /// Displays the project directory, environment, connection name, and the resolved connection string (masked by default).
    /// </summary>
    /// <param name="project">Path to the project directory to inspect; uses current directory when null.</param>
    /// <param name="connectionName">The named connection string to look up in configuration; uses the default when null.</param>
    /// <param name="environment">The environment name used to locate environment-specific configuration; may be null.</param>
    /// <param name="showConnection">When true, prints the full connection string; when false, prints a masked version.</param>
    public static void DisplayInfo(string? project, string? connectionName, string? environment, bool showConnection)
    {
        try
        {
            var workingDir = project ?? Directory.GetCurrentDirectory();
            Console.WriteLine($"Project Directory: {workingDir}");
            Console.WriteLine($"Environment: {environment}");
            Console.WriteLine($"Connection Name: {connectionName}\n");

            var configuration = BuildConfiguration(workingDir, environment);
            var connectionString = GetConnectionString(configuration, connectionName);

            if (connectionString != null)
            {
                Console.WriteLine("✓ Connection string found in appsettings.json");

                if (showConnection)
                {
                    Console.WriteLine($"\nConnection String:\n{connectionString}");
                }
                else
                {
                    var masked = MaskConnectionString(connectionString);
                    Console.WriteLine($"\nConnection String (masked):\n{masked}");
                    Console.WriteLine("\nUse --show-connection to display the full connection string.");
                }
            }
            else
            {
                Console.WriteLine($"✗ Connection string '{connectionName}' not found in appsettings.json");
                Console.WriteLine("\nChecked files:");
                Console.WriteLine($"  - appsettings.json");
                Console.WriteLine($"  - appsettings.{environment}.json");
            }

            Console.WriteLine($"\nConfiguration files location: {workingDir}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Masks sensitive information in a connection string.
    /// </summary>
    /// <param name="connectionString">The original connection string.</param>
    /// <summary>
    /// Returns the connection string with sensitive credential values (e.g., Password, Pwd, User ID, UID) obscured.
    /// </summary>
    /// <param name="connectionString">The connection string to mask.</param>
    /// <returns>The connection string with sensitive values replaced by up to eight asterisks.</returns>
    public static string MaskConnectionString(string connectionString)
    {
        var patterns = new[] { "Password=", "Pwd=", "User ID=", "UID=" };
        var result = connectionString;

        foreach (var pattern in patterns)
        {
            var index = result.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                var startIndex = index + pattern.Length;
                var endIndex = result.IndexOf(';', startIndex);
                if (endIndex < 0) endIndex = result.Length;

                var valueLength = endIndex - startIndex;
                result = result.Substring(0, startIndex) + new string('*', Math.Min(valueLength, 8)) + result.Substring(endIndex);
            }
        }

        return result;
    }

    /// <summary>
    /// Builds the configuration from appsettings files and environment variables.
    /// </summary>
    /// <param name="workingDirectory">The working directory.</param>
    /// <param name="environment">The environment name.</param>
    /// <summary>
    /// Builds an <see cref="IConfiguration"/> by loading appsettings.json, an optional environment-specific appsettings file, and environment variables using the provided working directory.
    /// </summary>
    /// <param name="workingDirectory">The base path used to locate appsettings files.</param>
    /// <param name="environment">The environment name to load environment-specific settings for; if null, defaults to "Development".</param>
    /// <returns>The constructed <see cref="IConfiguration"/>.</returns>
    public static IConfiguration BuildConfiguration(string workingDirectory, string? environment)
    {
        environment ??= "Development";

        var builder = new ConfigurationBuilder()
            .SetBasePath(workingDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    /// <summary>
    /// Retrieves the connection string from the configuration.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="connectionName">The name of the connection string.</param>
    /// <summary>
    /// Retrieves a named connection string from the provided configuration.
    /// </summary>
    /// <param name="connectionName">The name of the connection to retrieve; defaults to "DefaultConnection" when null.</param>
    /// <returns>The connection string for the specified name, or null if not found.</returns>
    public static string? GetConnectionString(IConfiguration configuration, string? connectionName)
    {
        connectionName ??= "DefaultConnection";
        return configuration.GetConnectionString(connectionName);
    }

    /// <summary>
    /// Executes the 'add migration' command.
    /// </summary>
    /// <param name="action">The action to perform.</param>
    /// <param name="name">The name of the migration.</param>
    /// <param name="context">The DbContext to use.</param>
    /// <param name="project">The project path.</param>
    /// <summary>
    /// Executes the Entity Framework CLI to add a migration for the specified project and context.
    /// </summary>
    /// <param name="action">The migration action to perform (typically "add").</param>
    /// <param name="name">The migration name to create.</param>
    /// <param name="context">The DbContext type name to target, or null to use the default context.</param>
    /// <param name="project">The project path or directory containing the target project; if null the current directory is used.</param>
    /// <param name="outputDir">The directory (relative to the project) where the generated migration files should be placed, or null to use the default location.</param>
    public static async Task ExecuteMigrationCommand(string action, string name, string? context, string? project, string? outputDir)
    {
        try
        {
            Console.WriteLine($"Adding migration '{name}'...");

            var args = new List<string> { "ef", "migrations", "add", name };

            if (!string.IsNullOrEmpty(context))
                args.AddRange(new[] { "--context", context });

            if (!string.IsNullOrEmpty(outputDir))
                args.AddRange(new[] { "--output-dir", outputDir });

            if (!string.IsNullOrEmpty(project))
                args.AddRange(new[] { "--project", project });

            var result = await ExecuteDotNetCommand([.. args], project);

            if (result == 0)
                Console.WriteLine($"✓ Migration '{name}' added successfully!");
            else
                Console.WriteLine($"✗ Failed to add migration.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Updates the database to the specified migration.
    /// </summary>
    /// <param name="migration">The target migration.</param>
    /// <param name="context">The DbContext to use.</param>
    /// <param name="project">The project path.</param>
    /// <param name="connection">The connection string.</param>
    /// <param name="connectionName">The connection string name.</param>
    /// <summary>
    /// Updates the database to a specific migration or to the latest migration using the dotnet EF CLI.
    /// </summary>
    /// <param name="migration">The target migration name; if null, updates to the latest migration.</param>
    /// <param name="context">The DbContext type name to target; optional.</param>
    /// <param name="project">The project directory or path containing the EF project; if null the current directory is used.</param>
    /// <param name="connection">An explicit connection string to use; if null the connection will be read from configuration.</param>
    /// <param name="connectionName">The configuration key for the connection string; defaults to "DefaultConnection" when not provided.</param>
    /// <param name="environment">The environment name used when loading appsettings (e.g., "Development"); defaults to "Development" when not provided.</param>
    public static async Task UpdateDatabase(string? migration, string? context, string? project, string? connection, string? connectionName, string? environment)
    {
        try
        {
            var workingDir = project ?? Directory.GetCurrentDirectory();

            // If no explicit connection provided, try to get from appsettings
            if (string.IsNullOrEmpty(connection))
            {
                var configuration = BuildConfiguration(workingDir, environment);
                connection = GetConnectionString(configuration, connectionName);

                if (connection != null)
                {
                    Console.WriteLine($"Using connection string '{connectionName ?? "DefaultConnection"}' from appsettings.json");
                }
            }

            Console.WriteLine($"Updating database{(migration != null ? $" to migration '{migration}'" : " to latest")}...");

            var args = new List<string> { "ef", "database", "update" };

            if (!string.IsNullOrEmpty(migration))
                args.Add(migration);

            if (!string.IsNullOrEmpty(context))
                args.AddRange(new[] { "--context", context });

            if (!string.IsNullOrEmpty(connection))
                args.AddRange(new[] { "--connection", connection });

            if (!string.IsNullOrEmpty(project))
                args.AddRange(new[] { "--project", project });

            var result = await ExecuteDotNetCommand([.. args], project);

            if (result == 0)
                Console.WriteLine("✓ Database updated successfully!");
            else
                Console.WriteLine("✗ Failed to update database.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Lists all available migrations.
    /// </summary>
    /// <param name="context">The DbContext to use.</param>
    /// <param name="project">The project path.</param>
    /// <param name="connection">The connection string.</param>
    /// <param name="connectionName">The connection string name.</param>
    /// <summary>
    /// Lists Entity Framework migrations for the specified project and context, printing results to the console.
    /// </summary>
    /// <param name="context">The EF DbContext name to target; null to use the default.</param>
    /// <param name="project">The project path containing the EF migrations; null to use the current directory.</param>
    /// <param name="connection">An explicit connection string to use; if null the configuration will be checked for a named connection.</param>
    /// <param name="connectionName">The name of the connection string to look up in configuration when <paramref name="connection"/> is null.</param>
    /// <param name="environment">The environment name used when loading configuration (e.g., "Development").</param>
    public static async Task ListMigrations(string? context, string? project, string? connection, string? connectionName, string? environment)
    {
        try
        {
            var workingDir = project ?? Directory.GetCurrentDirectory();

            // If no explicit connection provided, try to get from appsettings
            if (string.IsNullOrEmpty(connection))
            {
                var configuration = BuildConfiguration(workingDir, environment);
                connection = GetConnectionString(configuration, connectionName);

                if (connection != null)
                {
                    Console.WriteLine($"Using connection string '{connectionName ?? "DefaultConnection"}' from appsettings.json\n");
                }
            }

            Console.WriteLine("Listing migrations...\n");

            var args = new List<string> { "ef", "migrations", "list" };

            if (!string.IsNullOrEmpty(context))
                args.AddRange(new[] { "--context", context });

            if (!string.IsNullOrEmpty(connection))
                args.AddRange(new[] { "--connection", connection });

            if (!string.IsNullOrEmpty(project))
                args.AddRange(new[] { "--project", project });

            await ExecuteDotNetCommand(args.ToArray(), project);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes the last migration.
    /// </summary>
    /// <param name="context">The DbContext to use.</param>
    /// <param name="project">The project path.</param>
    /// <summary>
    /// Removes the last Entity Framework migration for the specified project and context.
    /// </summary>
    /// <param name="context">The DbContext type name to target; if null, the default context is used.</param>
    /// <param name="project">The project directory or project file path to run the command in; if null, the current directory is used.</param>
    /// <param name="force">If true, forces removal without prompting for confirmation.</param>
    /// <returns>A task that completes when the migration removal process has finished.</returns>
    public static async Task RemoveMigration(string? context, string? project, bool force)
    {
        try
        {
            Console.WriteLine("Removing last migration...");

            var args = new List<string> { "ef", "migrations", "remove" };

            if (!string.IsNullOrEmpty(context))
                args.AddRange(new[] { "--context", context });

            if (force)
                args.Add("--force");

            if (!string.IsNullOrEmpty(project))
                args.AddRange(new[] { "--project", project });

            var result = await ExecuteDotNetCommand(args.ToArray(), project);

            if (result == 0)
                Console.WriteLine("✓ Migration removed successfully!");
            else
                Console.WriteLine("✗ Failed to remove migration.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a SQL script for migrations.
    /// </summary>
    /// <param name="from">Starting migration.</param>
    /// <param name="to">Ending migration.</param>
    /// <param name="context">The DbContext to use.</param>
    /// <param name="project">The project path.</param>
    /// <param name="output">The output file path.</param>
    /// <param name="idempotent">Whether to generate an idempotent script.</param>
    /// <param name="connection">The connection string.</param>
    /// <param name="connectionName">The connection string name.</param>
    /// <summary>
    /// Generates a SQL script for EF Core migrations by invoking the dotnet-ef CLI and optionally writes it to a file.
    /// </summary>
    /// <param name="from">The starting migration identifier or null to generate from the initial state.</param>
    /// <param name="to">The target migration identifier or null to generate up to the latest migration.</param>
    /// <param name="context">The DbContext type name to target, or null to use the default context.</param>
    /// <param name="project">The project directory or file to run the command in, or null to use the current directory.</param>
    /// <param name="output">A file path to write the generated script to, or null to write to standard output.</param>
    /// <param name="idempotent">If true, generate an idempotent script that can be applied on any database state.</param>
    /// <param name="connection">An explicit connection string to use; if null the connection is retrieved from configuration.</param>
    /// <param name="connectionName">The named connection to look up in configuration when <paramref name="connection"/> is null; defaults to "DefaultConnection".</param>
    /// <param name="environment">The environment name used to select appsettings.{environment}.json when resolving configuration.</param>
    public static async Task ScriptMigration(string? from, string? to, string? context, string? project, string? output, bool idempotent, string? connection, string? connectionName, string? environment)
    {
        try
        {
            var workingDir = project ?? Directory.GetCurrentDirectory();

            // If no explicit connection provided, try to get from appsettings
            if (string.IsNullOrEmpty(connection))
            {
                var configuration = BuildConfiguration(workingDir, environment);
                connection = GetConnectionString(configuration, connectionName);

                if (connection != null)
                {
                    Console.WriteLine($"Using connection string '{connectionName ?? "DefaultConnection"}' from appsettings.json");
                }
            }

            Console.WriteLine("Generating migration script...");

            var args = new List<string> { "ef", "migrations", "script" };

            if (!string.IsNullOrEmpty(from))
                args.Add(from);

            if (!string.IsNullOrEmpty(to))
                args.Add(to);

            if (!string.IsNullOrEmpty(context))
                args.AddRange(new[] { "--context", context });

            if (!string.IsNullOrEmpty(output))
                args.AddRange(new[] { "--output", output });

            if (idempotent)
                args.Add("--idempotent");

            if (!string.IsNullOrEmpty(connection))
                args.AddRange(new[] { "--connection", connection });

            if (!string.IsNullOrEmpty(project))
                args.AddRange(new[] { "--project", project });

            var result = await ExecuteDotNetCommand(args.ToArray(), project);

            if (result == 0)
                Console.WriteLine($"✓ Script generated successfully{(!string.IsNullOrEmpty(output) ? $" at {output}" : "")}!");
            else
                Console.WriteLine("✗ Failed to generate script.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Executes a dotnet command.
    /// </summary>
    /// <param name="args">The command arguments.</param>
    /// <param name="workingDirectory">The working directory.</param>
    /// <summary>
    /// Execute the dotnet CLI with the provided arguments, streaming stdout and stderr to the console.
    /// </summary>
    /// <param name="args">Arguments to pass to the dotnet command (e.g., "ef", "migrations", "add", "Name").</param>
    /// <param name="workingDirectory">Working directory for the process; if null the current directory is used.</param>
    /// <returns>The process exit code.</returns>
    public static async Task<int> ExecuteDotNetCommand(string[] args, string? workingDirectory)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = string.Join(" ", args.Select(arg => $"\"{arg}\"")),
            WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = startInfo };

        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                Console.WriteLine(e.Data);
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                Console.Error.WriteLine(e.Data);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        await process.WaitForExitAsync();

        return process.ExitCode;
    }
}