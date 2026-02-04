namespace iiwi.Common.Privileges;

public class AuthenticationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Gets the LoadKeyAndQrCodeUri.
    /// </summary>
    public string LoadKeyAndQrCodeUri => $"{moduleName}.LoadKeyAndQrCodeUri";
    /// <summary>
    /// Gets the EnableAuthenticator.
    /// </summary>
    public string EnableAuthenticator => $"{moduleName}.EnableAuthenticator";
    /// <summary>
    /// Gets the ExternalLogins.
    /// </summary>
    public string ExternalLogins => $"{moduleName}.ExternalLogins";
    /// <summary>
    /// Gets the RemoveLogin.
    /// </summary>
    public string RemoveLogin => $"{moduleName}.RemoveLogin";
    /// <summary>
    /// Gets the LinkLogin.
    /// </summary>
    public string LinkLogin => $"{moduleName}.LinkLogin";
    /// <summary>
    /// Gets the LinkLoginCallback.
    /// </summary>
    public string LinkLoginCallback => $"{moduleName}.LinkLoginCallback";
    /// <summary>
    /// Gets the GenerateRecoveryCodes.
    /// </summary>
    public string GenerateRecoveryCodes => $"{moduleName}.GenerateRecoveryCodes";
    /// <summary>
    /// Gets the ResetAuthenticator.
    /// </summary>
    public string ResetAuthenticator => $"{moduleName}.ResetAuthenticator";
    /// <summary>
    /// Gets the SetPassword.
    /// </summary>
    public string SetPassword => $"{moduleName}.SetPassword";
    /// <summary>
    /// Gets the ChangePassword.
    /// </summary>
    public string ChangePassword => $"{moduleName}.ChangePassword";
    /// <summary>
    /// Gets the AccountStatus.
    /// </summary>
    public string AccountStatus => $"{moduleName}.AccountStatus";
    /// <summary>
    /// Gets the ForgotBrowser.
    /// </summary>
    public string ForgotBrowser => $"{moduleName}.ForgotBrowser";
    /// <summary>
    /// Gets the Disable2fa.
    /// </summary>
    public string Disable2fa => $"{moduleName}.Disable2fa";
    /// <summary>
    /// Gets the All.
    /// </summary>
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
