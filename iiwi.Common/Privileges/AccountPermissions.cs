namespace iiwi.Common.Privileges;

/// <summary>
/// Defines permissions related to account management.
/// </summary>
/// <param name="moduleName">The name of the module these permissions belong to.</param>
public class AccountPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Permission to update user profile.
    /// </summary>
    public string UpdateProfile => $"{moduleName}.UpdateProfile";

    /// <summary>
    /// Permission to download user information.
    /// </summary>
    public string DownloadInfo => $"{moduleName}.DownloadInfo";

    /// <summary>
    /// Permission to send verification details.
    /// </summary>
    public string SendVerificationDetails => $"{moduleName}.SendVerificationDetails";

    /// <summary>
    /// Permission to change email address.
    /// </summary>
    public string ChangeEmail => $"{moduleName}.ChangeEmail";

    /// <summary>
    /// Permission to delete personal data.
    /// </summary>
    public string DeletePersonalData => $"{moduleName}.DeletePersonalData";

    /// <summary>
    /// Permission to update phone number.
    /// </summary>
    public string UpdatePhoneNumber => $"{moduleName}.UpdatePhoneNumber";

    /// <summary>
    /// Gets all permission strings defined in this module.
    /// </summary>
    public override IEnumerable<string> All => base.All.Concat([
        UpdateProfile,
        DeletePersonalData, 
        DownloadInfo,
        SendVerificationDetails,
        ChangeEmail,
        UpdatePhoneNumber
        ]);
}
