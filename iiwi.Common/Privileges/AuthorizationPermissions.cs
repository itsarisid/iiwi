namespace iiwi.Common.Privileges;

/// <summary>
/// Defines permissions related to role-based authorization and claims.
/// </summary>
/// <param name="moduleName">The name of the module these permissions belong to.</param>
public class AuthorizationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    /// <summary>
    /// Permission to view all roles.
    /// </summary>
    public string AllRoles => $"{moduleName}.AllRoles";

    /// <summary>
    /// Permission to add a new role.
    /// </summary>
    public string AddRole => $"{moduleName}.AddRole";

    /// <summary>
    /// Permission to update an existing role.
    /// </summary>
    public string UpdateRole => $"{moduleName}.UpdateRole";

    /// <summary>
    /// Permission to delete a role.
    /// </summary>
    public string DeleteRole => $"{moduleName}.DeleteRole";

    /// <summary>
    /// Permission to view roles by ID.
    /// </summary>
    public string RolesById => $"{moduleName}.RolesById";

    /// <summary>
    /// Permission to add a claim.
    /// </summary>
    public string AddClaim => $"{moduleName}.AddClaim";

    /// <summary>
    /// Permission to add a claim to a role.
    /// </summary>
    public string AddRoleClaim => $"{moduleName}.AddRoleClaim";

    /// <summary>
    /// Permission to remove a claim from a role.
    /// </summary>
    public string RemoveRoleClaim => $"{moduleName}.RemoveRoleClaim";

    /// <summary>
    /// Permission to get claims associated with a role.
    /// </summary>
    public string GetRoleClaims => $"{moduleName}.GetRoleClaims";

    /// <summary>
    /// Permission to assign a role to a user.
    /// </summary>
    public string AssignRole => $"{moduleName}.AssignRole";

    /// <summary>
    /// Permission to add a claim to a user.
    /// </summary>
    public string AddUserClaim => $"{moduleName}.AddUserClaim";

    /// <summary>
    /// Gets all permission strings defined in this module.
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
