using Asp.Versioning;

namespace iiwi.NetLine.API;

public class TestEndpointConfiguration
{
    public string Author { get; set; } = "Sajid Khan";
    public string Version { get; set; } = "1.0.0";
    public double[] DeprecatedVersions { get; set; } = Array.Empty<double>();
    public ApiVersion[] ActiveVersions { get; set; } = Array.Empty<ApiVersion>();
    public string CachePolicy { get; set; } = "DefaultPolicy";
    public bool EnableHttpLogging { get; set; } = true;
}
