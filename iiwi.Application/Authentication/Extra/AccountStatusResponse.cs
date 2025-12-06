namespace iiwi.Application.Authentication;

/// <summary>
/// Response model for account status.
/// </summary>
public record AccountStatusResponse:Response
{
    /// <summary>
    /// Gets or sets a value indicating whether the user has an authenticator app configured.
    /// </summary>
    public bool HasAuthenticator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether two-factor authentication is enabled.
    /// </summary>
    public bool Is2faEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the current machine is remembered for 2FA.
    /// </summary>
    public bool IsMachineRemembered { get; set; }

    /// <summary>
    /// Gets or sets the number of recovery codes remaining.
    /// </summary>
    public int RecoveryCodesLeft { get; set; }
}
