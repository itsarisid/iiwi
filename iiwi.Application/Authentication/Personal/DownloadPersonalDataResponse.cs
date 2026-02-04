
using iiwi.Application;

namespace iiw.Application.Authentication;

public record DownloadPersonalDataResponse:Response
{
    /// <summary>
    /// Gets or sets the Data.
    /// </summary>
    public Dictionary<string, string> Data { get; set; }
}
