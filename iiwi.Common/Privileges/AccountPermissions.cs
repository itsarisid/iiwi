namespace iiwi.Common.Privileges;

public class AccountPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    public string UpdateProfile => $"{moduleName}.UpdateProfile";
    public string DownloadInfo => $"{moduleName}.DownloadInfo";
    public string SendVerificationDetails => $"{moduleName}.SendVerificationDetails";
    public string ChangeEmail => $"{moduleName}.ChangeEmail";
    public string DeletePersonalData => $"{moduleName}.DeletePersonalData";
    public string UpdatePhoneNumber => $"{moduleName}.UpdatePhoneNumber";

    public override IEnumerable<string> All => base.All.Concat([
        UpdateProfile,
        DeletePersonalData, 
        DownloadInfo,
        SendVerificationDetails,
        ChangeEmail,
        UpdatePhoneNumber
        ]);
}
