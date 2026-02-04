namespace iiwi.Application.Authentication;

public record GenerateRecoveryCodesResponse:Response
{
    /// <summary>
    /// Gets or sets the RecoveryCodes.
    /// </summary>
    public string[] RecoveryCodes { get; set; }
}
