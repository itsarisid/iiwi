using iiwi.Application;
using System.Runtime.InteropServices;

namespace iiwi.Model.PingPong;

public record SystemInfoResponse : Response
{
    public string Version { get; set; } = "1.0.0";
    public string Date => DateTime.Now.ToLongDateString();
    public string Time => DateTime.Now.ToLongTimeString();
    public string? Assembly => System.Reflection.Assembly.GetExecutingAssembly().FullName;
    public string MachineName { get; set; } = string.Empty;
    public string Framework => RuntimeInformation.FrameworkDescription;
    public string OS => $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})";
    public string Author { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
}
