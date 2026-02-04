namespace iiwi.Common.Privileges;

public class AuthorizationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Gets the AllRoles.
    /// </summary>
    public string AllRoles => $"{moduleName}.AllRoles";
    /// <summary>
    /// Gets the AddRole.
    /// </summary>
    public string AddRole => $"{moduleName}.AddRole";
    /// <summary>
    /// Gets the UpdateRole.
    /// </summary>
    public string UpdateRole => $"{moduleName}.UpdateRole";
    /// <summary>
    /// Gets the DeleteRole.
    /// </summary>
    public string DeleteRole => $"{moduleName}.DeleteRole";
    /// <summary>
    /// Gets the RolesById.
    /// </summary>
    public string RolesById => $"{moduleName}.RolesById";
    /// <summary>
    /// Gets the AddClaim.
    /// </summary>
    public string AddClaim => $"{moduleName}.AddClaim";
    /// <summary>
    /// Gets the AddRoleClaim.
    /// </summary>
    public string AddRoleClaim => $"{moduleName}.AddRoleClaim";
    /// <summary>
    /// Gets the RemoveRoleClaim.
    /// </summary>
    public string RemoveRoleClaim => $"{moduleName}.RemoveRoleClaim";
    /// <summary>
    /// Gets the GetRoleClaims.
    /// </summary>
    public string GetRoleClaims => $"{moduleName}.GetRoleClaims";
    /// <summary>
    /// Gets the AssignRole.
    /// </summary>
    public string AssignRole => $"{moduleName}.AssignRole";
    /// <summary>
    /// Gets the AddUserClaim.
    /// </summary>
    public string AddUserClaim => $"{moduleName}.AssignRole";
    /// <summary>
    /// Gets the All.
    /// </summary>
    public override IEnumerable<string> All => base.All.Concat([
        AllRoles,
        AddRole,
        UpdateRole,
        DeleteRole,
        RolesById,
        AddClaim,
        AddRoleClaim,
        RemoveRoleClaim,
        GetRoleClaims,
        AssignRole,
        AddUserClaim
        ]);
}
