
namespace iiwi.Application.Authentication;

/// <summary>
/// Request model for removing an external login.
/// </summary>
public record RemoveLoginRequest
{
    /// <summary>
    /// Gets or sets the login provider.
    /// </summary>
    public string LoginProvider { get; set; }

    /// <summary>
    /// Gets or sets the provider key.
    /// </summary>
    public string ProviderKey { get; set; }
}
