namespace iiwi.Application.Authentication;

public record AccountStatusResponse:Response
{
    /// <summary>
    /// Gets or sets the HasAuthenticator.
    /// </summary>
    public bool HasAuthenticator { get; set; }
    /// <summary>
    /// Gets or sets the Is2faEnabled.
    /// </summary>
    public bool Is2faEnabled { get; set; }
    /// <summary>
    /// Gets or sets the IsMachineRemembered.
    /// </summary>
    public bool IsMachineRemembered { get; set; }
    /// <summary>
    /// Gets or sets the RecoveryCodesLeft.
    /// </summary>
    public int RecoveryCodesLeft { get; set; }
}
