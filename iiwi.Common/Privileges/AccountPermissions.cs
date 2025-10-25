namespace iiwi.Common.Privileges;

public class AccountPermissions(string moduleName) : PermissionModule(moduleName)
{
    public string UpdateProfile => $"{moduleName}.UpdateProfile";
    public string DeleteData => $"{moduleName}.DeleteData";
    public string DownloadInfo => $"{moduleName}.DownloadInfo";
    public string SendVerificationDetails => $"{moduleName}.SendVerificationDetails";

    public override IEnumerable<string> All => base.All.Concat([
        UpdateProfile, 
        DeleteData, 
        DownloadInfo,
        SendVerificationDetails
        ]);
}
