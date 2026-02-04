using iiwi.Application;
using System.Runtime.InteropServices;

namespace iiwi.Model.PingPong;

public record SystemInfoResponse : Response
{
    /// <summary>
    /// Gets or sets the Version.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    /// <summary>
    /// Gets the Date.
    /// </summary>
    public string Date => DateTime.Now.ToLongDateString();
    /// <summary>
    /// Gets the Time.
    /// </summary>
    public string Time => DateTime.Now.ToLongTimeString();
    /// <summary>
    /// Gets the Assembly.
    /// </summary>
    public string? Assembly => System.Reflection.Assembly.GetExecutingAssembly().FullName;
    /// <summary>
    /// Gets or sets the MachineName.
    /// </summary>
    public string MachineName { get; set; } = string.Empty;
    /// <summary>
    /// Gets the Framework.
    /// </summary>
    public string Framework => RuntimeInformation.FrameworkDescription;
    /// <summary>
    /// Gets the OS.
    /// </summary>
    public string OS => $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})";
    /// <summary>
    /// Gets or sets the Author.
    /// </summary>
    public string Author { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Environment.
    /// </summary>
    public string Environment { get; set; } = string.Empty;
}
