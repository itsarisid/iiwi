namespace iiwi.Common.Privileges;

public class AccountPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Gets the UpdateProfile.
    /// </summary>
    public string UpdateProfile => $"{moduleName}.UpdateProfile";
    /// <summary>
    /// Gets the DownloadInfo.
    /// </summary>
    public string DownloadInfo => $"{moduleName}.DownloadInfo";
    /// <summary>
    /// Gets the SendVerificationDetails.
    /// </summary>
    public string SendVerificationDetails => $"{moduleName}.SendVerificationDetails";
    /// <summary>
    /// Gets the ChangeEmail.
    /// </summary>
    public string ChangeEmail => $"{moduleName}.ChangeEmail";
    /// <summary>
    /// Gets the DeletePersonalData.
    /// </summary>
    public string DeletePersonalData => $"{moduleName}.DeletePersonalData";
    /// <summary>
    /// Gets the UpdatePhoneNumber.
    /// </summary>
    public string UpdatePhoneNumber => $"{moduleName}.UpdatePhoneNumber";

    /// <summary>
    /// Gets the All.
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
