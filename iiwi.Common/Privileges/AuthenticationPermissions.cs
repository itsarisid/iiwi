namespace iiwi.Common.Privileges;

public class AuthenticationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    public string LoadKeyAndQrCodeUri => $"{moduleName}.LoadKeyAndQrCodeUri";
    public string EnableAuthenticator => $"{moduleName}.EnableAuthenticator";
    public string ExternalLogins => $"{moduleName}.ExternalLogins";
    public string RemoveLogin => $"{moduleName}.RemoveLogin";
    public string LinkLogin => $"{moduleName}.LinkLogin";
    public string LinkLoginCallback => $"{moduleName}.LinkLoginCallback";
    public string GenerateRecoveryCodes => $"{moduleName}.GenerateRecoveryCodes";
    public string ResetAuthenticator => $"{moduleName}.ResetAuthenticator";
    public string SetPassword => $"{moduleName}.SetPassword";
    public string ChangePassword => $"{moduleName}.ChangePassword";
    public string AccountStatus => $"{moduleName}.AccountStatus";
    public string ForgotBrowser => $"{moduleName}.ForgotBrowser";
    public string Disable2fa => $"{moduleName}.Disable2fa";
    public override IEnumerable<string> All => base.All.Concat([
        LoadKeyAndQrCodeUri,
        ExternalLogins,
        RemoveLogin, LinkLogin,LinkLoginCallback,
        GenerateRecoveryCodes,
        ResetAuthenticator,
        SetPassword,
        ChangePassword,
        AccountStatus,
        ForgotBrowser,
        Disable2fa
        ]);
}
