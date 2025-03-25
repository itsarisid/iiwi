using Serilog.Sinks.SystemConsole.Themes;

namespace iiwi.AppWire.Configurations;

public class SeriLogCustomThemes
{
    public static SystemConsoleTheme Theme()
    {
        Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> customThemeStyles = new()
            {
                {
                    ConsoleThemeStyle.Text, new SystemConsoleThemeStyle
                    {
                        Foreground = ConsoleColor.Green,
                    }
                },
                {
                    ConsoleThemeStyle.String, new SystemConsoleThemeStyle
                    {
                        Foreground = ConsoleColor.Yellow,
                    }
                },
                {
                    ConsoleThemeStyle.Name, new SystemConsoleThemeStyle
                    {
                        Foreground = ConsoleColor.Black,
                        Background = ConsoleColor.Yellow,
                    }
                }

            };

        return new SystemConsoleTheme(customThemeStyles);
    }
}
