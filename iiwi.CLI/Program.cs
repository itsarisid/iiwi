using iiwi.CLI;
using System.CommandLine;

var rootCommand = new RootCommand("Database Migration CLI for IIWI");

// Migration commands
var addCommand = Helper.CreateAddMigrationCommand();
var updateCommand = Helper.CreateUpdateDatabaseCommand();
var listCommand = Helper.CreateListMigrationsCommand();
var removeCommand = Helper.CreateRemoveMigrationCommand();
var scriptCommand = Helper.CreateScriptMigrationCommand();
var infoCommand = Helper.CreateInfoCommand();

rootCommand.AddCommand(addCommand);
rootCommand.AddCommand(updateCommand);
rootCommand.AddCommand(listCommand);
rootCommand.AddCommand(removeCommand);
rootCommand.AddCommand(scriptCommand);
rootCommand.AddCommand(infoCommand);

return await rootCommand.InvokeAsync(args);