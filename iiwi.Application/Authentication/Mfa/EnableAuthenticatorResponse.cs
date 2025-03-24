namespace iiwi.Application.Authentication;

public record EnableAuthenticatorResponse:Response
{
    /// <summary>Gets or sets the shared key.</summary>
    /// <value>The shared key.</value>
    public string SharedKey { get; set; }

    /// <summary>Gets or sets the authenticator URI.</summary>
    /// <value>The authenticator URI.</value>
    public string AuthenticatorUri { get; set; }

    /// <summary>Gets or sets the recovery codes.</summary>
    /// <value>The recovery codes.</value>
    public string[] RecoveryCodes { get; set; }

    /// <summary>Gets or sets the status message.</summary>
    /// <value>The status message.</value>
    public string StatusMessage { get; set; }
}
