
using iiwi.Application;

namespace iiwi.Application.Authentication;

/// <summary>
/// Response model for downloading personal data.
/// </summary>
public record DownloadPersonalDataResponse:Response
{
    /// <summary>
    /// Gets or sets the personal data.
    /// </summary>
    public Dictionary<string, string> Data { get; set; }
}
