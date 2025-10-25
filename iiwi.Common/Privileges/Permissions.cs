namespace iiwi.Common.Privileges;


public static class Permissions
{
    public const string Permission = "Permission";

    public static readonly PermissionModule Test = PermissionModule.For("Test");
    //public static readonly PermissionModule Account = PermissionModule.For("Account");
    public static readonly AccountPermissions Account = PermissionModule.ForAccount("Account");

    public static IEnumerable<string> GetAll() =>
       AllPermissionModules.SelectMany(module => module);

    private static IEnumerable<IEnumerable<string>> AllPermissionModules =>
        [Test.All, Account.All];
}
