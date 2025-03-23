
using iiwi.Application;

namespace iiw.Application.Authentication;

public record DownloadPersonalDataResponse:Response
{
    public Dictionary<string, string> Data { get; set; }
}
