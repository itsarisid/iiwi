namespace iiwi.Common.Privileges;

public class PermissionModule(string moduleName)
{
    /// <summary>
    /// Gets the Read.
    /// </summary>
    public string Read => $"{moduleName}.Read";
    /// <summary>
    /// Gets the Create.
    /// </summary>
    public string Create => $"{moduleName}.Create";
    /// <summary>
    /// Gets the Update.
    /// </summary>
    public string Update => $"{moduleName}.Update";
    /// <summary>
    /// Gets the Delete.
    /// </summary>
    public string Delete => $"{moduleName}.Delete";

    /// <summary>
    /// Gets the All.
    /// </summary>
    public virtual IEnumerable<string> All => [Read, Create, Update, Delete];

    /// <summary>
    /// Executes the For operation.
    /// </summary>
    public static PermissionModule For(string moduleName) => new(moduleName);

    public static TPermissions Permissions<TPermissions>(string moduleName)
        where TPermissions : PermissionModule => (TPermissions)Activator.CreateInstance(typeof(TPermissions), moduleName)!;
    
}