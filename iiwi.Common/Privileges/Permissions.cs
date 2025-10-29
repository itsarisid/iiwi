namespace iiwi.Common.Privileges;


public static class Permissions
{
    public const string Permission = "Permission";

    public static readonly PermissionModule Test = PermissionModule.For("Test");

    public static readonly AccountPermissions Account = PermissionModule.Permissions<AccountPermissions>("Account");
    public static readonly AuthenticationPermissions Authentication = PermissionModule.Permissions<AuthenticationPermissions>("Authentication");
    public static readonly AuthorizationPermissions Authorization = PermissionModule.Permissions<AuthorizationPermissions>("Authorization");

    public static IEnumerable<string> GetAll() =>
       AllPermissionModules.SelectMany(module => module);

    private static IEnumerable<IEnumerable<string>> AllPermissionModules =>
        [
            Test.All, 
            Account.All, 
            Authentication.All, 
            Authorization.All
        ];
}
