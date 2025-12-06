namespace iiwi.Common.Privileges;


/// <summary>
/// Central registry of all application permissions.
/// </summary>
public static class Permissions
{
    /// <summary>
    /// The claim type for permissions.
    /// </summary>
    public const string Permission = "Permission";

    /// <summary>
    /// Permissions for the Test module.
    /// </summary>
    public static readonly PermissionModule Test = PermissionModule.For("Test");

    /// <summary>
    /// Permissions for the Account module.
    /// </summary>
    public static readonly AccountPermissions Account = PermissionModule.Permissions<AccountPermissions>("Account");

    /// <summary>
    /// Permissions for the Authentication module.
    /// </summary>
    public static readonly AuthenticationPermissions Authentication = PermissionModule.Permissions<AuthenticationPermissions>("Authentication");

    /// <summary>
    /// Permissions for the Authorization module.
    /// </summary>
    public static readonly AuthorizationPermissions Authorization = PermissionModule.Permissions<AuthorizationPermissions>("Authorization");

    /// <summary>
    /// Gets a flat list of all permissions defined in the application.
    /// </summary>
    /// <returns>An enumerable of permission strings.</returns>
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
