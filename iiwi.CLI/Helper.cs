using Microsoft.Extensions.Configuration;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;

namespace iiwi.CLI;

public static class Helper
{
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

    public static string? GetConnectionString(IConfiguration configuration, string? connectionName)
    {
        connectionName ??= "DefaultConnection";
        return configuration.GetConnectionString(connectionName);
    }

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
