
namespace Architecture.Application.Authentication
{
    public record DownloadPersonalDataResponse:Response
    {
        public Dictionary<string, string> Data { get; set; }
    }
}
