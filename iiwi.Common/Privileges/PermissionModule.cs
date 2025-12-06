namespace iiwi.Common.Privileges;

/// <summary>
/// Base class for permission modules, providing standard CRUD permissions.
/// </summary>
/// <param name="moduleName">The name of the module.</param>
public class PermissionModule(string moduleName)
{
    /// <summary>
    /// Permission to read resources in this module.
    /// </summary>
    public string Read => $"{moduleName}.Read";

    /// <summary>
    /// Permission to create resources in this module.
    /// </summary>
    public string Create => $"{moduleName}.Create";

    /// <summary>
    /// Permission to update resources in this module.
    /// </summary>
    public string Update => $"{moduleName}.Update";

    /// <summary>
    /// Permission to delete resources in this module.
    /// </summary>
    public string Delete => $"{moduleName}.Delete";

    /// <summary>
    /// Gets all standard CRUD permissions (Read, Create, Update, Delete).
    /// </summary>
    public virtual IEnumerable<string> All => [Read, Create, Update, Delete];

    /// <summary>
    /// Creates a generic PermissionModule for the specified module name.
    /// </summary>
    /// <param name="moduleName">The name of the module.</param>
    /// <returns>A new PermissionModule instance.</returns>
    public static PermissionModule For(string moduleName) => new(moduleName);

    /// <summary>
    /// Creates a specific permission module instance.
    /// </summary>
    /// <typeparam name="TPermissions">The type of the permission module.</typeparam>
    /// <param name="moduleName">The name of the module.</param>
    /// <returns>An instance of the specified permission module type.</returns>
    public static TPermissions Permissions<TPermissions>(string moduleName)
        where TPermissions : PermissionModule => (TPermissions)Activator.CreateInstance(typeof(TPermissions), moduleName)!;
    
}