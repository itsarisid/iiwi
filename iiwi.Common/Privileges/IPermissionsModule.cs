namespace iiwi.Common.Privileges;

/// <summary>
/// Interface for permission modules.
/// </summary>
public interface IPermissionsModule
{
    /// <summary>
    /// Gets all permission strings defined in the module.
    /// </summary>
    IEnumerable<string> All { get; }
}
