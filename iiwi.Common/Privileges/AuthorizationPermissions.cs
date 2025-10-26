namespace iiwi.Common.Privileges;

public class AuthorizationPermissions(string moduleName) : PermissionModule(moduleName), IPermissionsModule
{
    public string AllRoles => $"{moduleName}.AllRoles";
    public string AddRole => $"{moduleName}.AddRole";
    public string UpdateRole => $"{moduleName}.UpdateRole";
    public string DeleteRole => $"{moduleName}.DeleteRole";
    public string RolesById => $"{moduleName}.RolesById";
    public string AddClaim => $"{moduleName}.AddClaim";
    public string AddRoleClaim => $"{moduleName}.AddRoleClaim";
    public string RemoveRoleClaim => $"{moduleName}.RemoveRoleClaim";
    public string GetRoleClaims => $"{moduleName}.GetRoleClaims";
    public string AssignRole => $"{moduleName}.AssignRole";
    public string AddUserClaim => $"{moduleName}.AssignRole";
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
