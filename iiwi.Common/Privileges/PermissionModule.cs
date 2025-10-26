namespace iiwi.Common.Privileges;

public class PermissionModule(string moduleName)
{
    public string Read => $"{moduleName}.Read";
    public string Create => $"{moduleName}.Create";
    public string Update => $"{moduleName}.Update";
    public string Delete => $"{moduleName}.Delete";

    public virtual IEnumerable<string> All => [Read, Create, Update, Delete];

    public static PermissionModule For(string moduleName) => new(moduleName);

    public static TPermissions Permissions<TPermissions>(string moduleName)
        where TPermissions : PermissionModule => (TPermissions)Activator.CreateInstance(typeof(TPermissions), moduleName)!;
    
}