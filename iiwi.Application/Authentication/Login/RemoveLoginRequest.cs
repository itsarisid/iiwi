
namespace iiwi.Application.Authentication;

public record RemoveLoginRequest
{
    /// <summary>
    /// Gets or sets the LoginProvider.
    /// </summary>
    public string LoginProvider { get; set; }
    /// <summary>
    /// Gets or sets the ProviderKey.
    /// </summary>
    public string ProviderKey { get; set; }
}
