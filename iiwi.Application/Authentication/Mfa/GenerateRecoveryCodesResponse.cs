namespace iiwi.Application.Authentication;

public record GenerateRecoveryCodesResponse:Response
{
    public string[] RecoveryCodes { get; set; }
}
