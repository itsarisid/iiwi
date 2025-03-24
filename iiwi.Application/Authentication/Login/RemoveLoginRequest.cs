
namespace iiwi.Application.Authentication;

public record RemoveLoginRequest
{
    public string LoginProvider { get; set; }
    public string ProviderKey { get; set; }
}
