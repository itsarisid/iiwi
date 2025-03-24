namespace iiwi.Application.Authentication;

public record AccountStatusResponse:Response
{
    public bool HasAuthenticator { get; set; }
    public bool Is2faEnabled { get; set; }
    public bool IsMachineRemembered { get; set; }
    public int RecoveryCodesLeft { get; set; }
}
