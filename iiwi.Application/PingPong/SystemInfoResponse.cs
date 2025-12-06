using iiwi.Application;
using System.Runtime.InteropServices;

namespace iiwi.Model.PingPong;

/// <summary>
/// Response model for system information.
/// </summary>
public record SystemInfoResponse : Response
{
    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    public string Version { get; set; } = "1.0.0";

    /// <summary>
    /// Gets the date.
    /// </summary>
    public string Date => DateTime.Now.ToLongDateString();

    /// <summary>
    /// Gets the time.
    /// </summary>
    public string Time => DateTime.Now.ToLongTimeString();

    /// <summary>
    /// Gets the assembly name.
    /// </summary>
    public string? Assembly => System.Reflection.Assembly.GetExecutingAssembly().FullName;

    /// <summary>
    /// Gets or sets the machine name.
    /// </summary>
    public string MachineName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the framework description.
    /// </summary>
    public string Framework => RuntimeInformation.FrameworkDescription;

    /// <summary>
    /// Gets the OS description.
    /// </summary>
    public string OS => $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})";

    /// <summary>
    /// Gets or sets the author.
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the environment name.
    /// </summary>
    public string Environment { get; set; } = string.Empty;
}
