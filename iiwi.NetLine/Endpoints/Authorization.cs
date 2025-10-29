using iiwi.Model;
namespace iiwi.NetLine.Endpoints;
/// <summary>
/// Contains endpoints for role-based access control (RBAC) and authorization management
/// </summary>
/// <remarks>
/// This group handles all authorization-related operations including:
/// <list type="bullet">
/// <item><description>Role management (CRUD operations)</description></item>
/// <item><description>Claim/permission management</description></item>
/// <item><description>Role-claim assignments</description></item>
/// <item><description>User-role assignments</description></item>
/// </list>
/// All endpoints require administrative privileges.
/// </remarks>
public static class Authorization
{
    /// <summary>
    /// Group information for Authorization endpoints
    /// </summary>
    /// <remarks>
    /// Collection of endpoints that manage the complete RBAC system:
    /// <list type="bullet">
    /// <item><description>Role lifecycle management</description></item>
    /// <item><description>Permission/claim definitions</description></item>
    /// <item><description>Role-user assignments</description></item>
    /// <item><description>Role-permission mappings</description></item>
    /// </list>
    /// </remarks>
    public static EndpointDetails Group => new()
    {
        Name = "Authorization",
        Tags = "Authorization",
        Summary = "RBAC Management",
        Description = "Endpoints for managing roles, permissions, and access control."
    };

    /// <summary>
    /// Retrieves all defined roles
    /// </summary>
    /// <remarks>
    /// Returns a complete list of all system roles with:
    /// <list type="bullet">
    /// <item><description>Role IDs and names</description></item>
    /// <item><description>Role descriptions</description></item>
    /// <item><description>Creation timestamps</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AllRoles => new()
    {
        Endpoint = "/roles",
        Name = "Get All Roles",
        Summary = "List roles",
        Description = "Retrieves all defined roles in the system."
    };

    /// <summary>
    /// Creates a new role
    /// </summary>
    /// <remarks>
    /// Accepts role definition including:
    /// <list type="bullet">
    /// <item><description>Role name (unique identifier)</description></item>
    /// <item><description>Role description</description></item>
    /// <item><description>Optional default permissions</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AddRole => new()
    {
        Endpoint = "/role",
        Name = "Create Role",
        Summary = "Add role",
        Description = "Creates a new role definition in the system."
    };

    /// <summary>
    /// Updates an existing role
    /// </summary>
    /// <remarks>
    /// Modifies role properties including:
    /// <list type="bullet">
    /// <item><description>Role name</description></item>
    /// <item><description>Description</description></item>
    /// </list>
    /// Does not modify role assignments or permissions.
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails UpdateRole => new()
    {
        Endpoint = "/role/{id}",
        Name = "Update Role",
        Summary = "Modify role",
        Description = "Updates properties of an existing role."
    };

    /// <summary>
    /// Deletes a role definition
    /// </summary>
    /// <remarks>
    /// Removes role from system after:
    /// <list type="bullet">
    /// <item><description>Revoking from all users</description></item>
    /// <item><description>Removing all permissions</description></item>
    /// </list>
    /// System/default roles cannot be deleted.
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails DeleteRole => new()
    {
        Endpoint = "/role/{id}",
        Name = "Delete Role",
        Summary = "Remove role",
        Description = "Permanently deletes a role definition."
    };

    /// <summary>
    /// Retrieves specific role details
    /// </summary>
    /// <remarks>
    /// Returns complete role information including:
    /// <list type="bullet">
    /// <item><description>Role properties</description></item>
    /// <item><description>Assigned permissions</description></item>
    /// <item><description>User assignment count</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails RolesById => new()
    {
        Endpoint = "/role/{id}",
        Name = "Get Role",
        Summary = "View role",
        Description = "Retrieves detailed information about a specific role."
    };

    /// <summary>
    /// Creates a new permission claim
    /// </summary>
    /// <remarks>
    /// Defines a new system permission with:
    /// <list type="bullet">
    /// <item><description>Claim type</description></item>
    /// <item><description>Claim value</description></item>
    /// <item><description>Description</description></item>
    /// </list>
    /// Claims are typically formatted as "resource:action".
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AddClaim => new()
    {
        Endpoint = "/claim",
        Name = "Create Claim",
        Summary = "Add permission",
        Description = "Defines a new permission claim in the system."
    };

    /// <summary>
    /// Updates a permission claim
    /// </summary>
    /// <remarks>
    /// Modifies claim properties including:
    /// <list type="bullet">
    /// <item><description>Description</description></item>
    /// <item><description>Display name</description></item>
    /// </list>
    /// Cannot modify claim type/value once created.
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails UpdateClaim => new()
    {
        Endpoint = "/claim/{id}",
        Name = "Update Claim",
        Summary = "Modify permission",
        Description = "Updates properties of an existing permission claim."
    };

    /// <summary>
    /// Deletes a permission claim
    /// </summary>
    /// <remarks>
    /// Removes claim from system after:
    /// <list type="bullet">
    /// <item><description>Revoking from all roles</description></item>
    /// <item><description>Revoking from all users</description></item>
    /// </list>
    /// System/default claims cannot be deleted.
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails DeleteClaim => new()
    {
        Endpoint = "/claim/{id}",
        Name = "Delete Claim",
        Summary = "Remove permission",
        Description = "Permanently deletes a permission claim."
    };

    /// <summary>
    /// Revokes permission from role
    /// </summary>
    /// <remarks>
    /// Removes specific permission from role's allowed claims.
    /// <para>
    /// Effects are immediate and affect:
    /// </para>
    /// <list type="bullet">
    /// <item><description>All users assigned the role</description></item>
    /// <item><description>Future permission checks</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails RemoveRoleClaim => new()
    {
        Endpoint = "/{roleId}/claim/{id}",
        Name = "Remove Role Claim",
        Summary = "Revoke permission",
        Description = "Removes a permission from a role's allowed claims."
    };

    /// <summary>
    /// Grants permission to role
    /// </summary>
    /// <remarks>
    /// Adds specific permission to role's allowed claims.
    /// <para>
    /// Effects are immediate and affect:
    /// </para>
    /// <list type="bullet">
    /// <item><description>All users assigned the role</description></item>
    /// <item><description>Future permission checks</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AddRoleClaim => new()
    {
        Endpoint = "/{roleId}/claim",
        Name = "Add Role Claim",
        Summary = "Grant permission",
        Description = "Adds a permission to a role's allowed claims."
    };

    /// <summary>
    /// Assigns role to user
    /// </summary>
    /// <remarks>
    /// Grants role membership to specified user account.
    /// <para>
    /// User immediately gains:
    /// </para>
    /// <list type="bullet">
    /// <item><description>All permissions associated with the role</description></item>
    /// <item><description>Role-based access rights</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AssignRole => new()
    {
        Endpoint = "/users/assign-role",
        Name = "Assign User Role",
        Summary = "Assign role",
        Description = "Grants a role to a specific user account."
    };

    /// <summary>
    /// Grants direct permission to user
    /// </summary>
    /// <remarks>
    /// Assigns permission claim directly to user account.
    /// <para>
    /// Bypasses role assignments for:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Special case permissions</description></item>
    /// <item><description>Temporary elevated access</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails AddUserClaim => new()
    {
        Endpoint = "/users/add-claim",
        Name = "Add User Claim",
        Summary = "Grant user permission",
        Description = "Assigns a permission directly to a user account."
    };

    /// <summary>
    /// Retrieves all permissions for role
    /// </summary>
    /// <remarks>
    /// Returns complete list of permissions including:
    /// <list type="bullet">
    /// <item><description>Directly assigned claims</description></item>
    /// <item><description>Inherited permissions</description></item>
    /// <item><description>Permission metadata</description></item>
    /// </list>
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails GetRoleClaims => new()
    {
        Endpoint = "/roles/{roleId}/claims",
        Name = "Get Role Claims",
        Summary = "List role permissions",
        Description = "Retrieves all permissions assigned to a specific role."
    };

    /// <summary>
    /// Checks role permissions
    /// </summary>
    /// <remarks>
    /// Verifies and returns:
    /// <list type="bullet">
    /// <item><description>Effective permissions for role</description></item>
    /// <item><description>Permission status (granted/denied)</description></item>
    /// <item><description>Permission sources</description></item>
    /// </list>
    /// Useful for debugging access control.
    /// Requires admin privileges.
    /// </remarks>
    public static EndpointDetails Permissions => new()
    {
        Endpoint = "/role/{id}/permissions",
        Name = "Check Permissions",
        Summary = "Verify access",
        Description = "Checks and returns effective permissions for a role."
    };
}