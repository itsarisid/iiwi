namespace iiwi.Common.Privileges;

/// <summary>
/// Defines permissions related to authentication and security.
/// </summary>
/// <param name="moduleName">The name of the module these permissions belong to.</param>
public class AuthenticationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Permission to load authenticator key and QR code URI.
    /// </summary>
    public string LoadKeyAndQrCodeUri => $"{moduleName}.LoadKeyAndQrCodeUri";

    /// <summary>
    /// Permission to enable two-factor authentication.
    /// </summary>
    public string EnableAuthenticator => $"{moduleName}.EnableAuthenticator";

    /// <summary>
    /// Permission to manage external logins.
    /// </summary>
    public string ExternalLogins => $"{moduleName}.ExternalLogins";

    /// <summary>
    /// Permission to remove an external login.
    /// </summary>
    public string RemoveLogin => $"{moduleName}.RemoveLogin";

    /// <summary>
    /// Permission to link an external login.
    /// </summary>
    public string LinkLogin => $"{moduleName}.LinkLogin";

    /// <summary>
    /// Permission to handle link login callback.
    /// </summary>
    public string LinkLoginCallback => $"{moduleName}.LinkLoginCallback";

    /// <summary>
    /// Permission to generate recovery codes.
    /// </summary>
    public string GenerateRecoveryCodes => $"{moduleName}.GenerateRecoveryCodes";

    /// <summary>
    /// Permission to reset the authenticator app key.
    /// </summary>
    public string ResetAuthenticator => $"{moduleName}.ResetAuthenticator";

    /// <summary>
    /// Permission to set a password (if one doesn't exist).
    /// </summary>
    public string SetPassword => $"{moduleName}.SetPassword";

    /// <summary>
    /// Permission to change the password.
    /// </summary>
    public string ChangePassword => $"{moduleName}.ChangePassword";

    /// <summary>
    /// Permission to view account status.
    /// </summary>
    public string AccountStatus => $"{moduleName}.AccountStatus";

    /// <summary>
    /// Permission to forget the current browser/device.
    /// </summary>
    public string ForgotBrowser => $"{moduleName}.ForgotBrowser";

    /// <summary>
    /// Permission to disable two-factor authentication.
    /// </summary>
    public string Disable2fa => $"{moduleName}.Disable2fa";

    /// <summary>
    /// Gets all permission strings defined in this module.
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
