namespace iiwi.Application.Authentication;

/// <summary>
/// Response model for generating recovery codes.
/// </summary>
public record GenerateRecoveryCodesResponse:Response
{
    /// <summary>
    /// Gets or sets the recovery codes.
    /// </summary>
    public string[] RecoveryCodes { get; set; }
}
