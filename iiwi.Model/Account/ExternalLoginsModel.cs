
using Microsoft.AspNetCore.Identity;

namespace iiwi.Model.Account;

/// <summary>
/// Model for external logins.
/// </summary>
/// <typeparam name="T">The type of login info.</typeparam>
public class ExternalLoginsModel<T>
{
    /// <summary>
    /// Gets or sets the current logins.
    /// </summary>
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    /// <summary>
    /// Gets or sets other logins.
    /// </summary>
    public IList<T> OtherLogins { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the remove button.
    /// </summary>
    public bool ShowRemoveButton { get; set; }

    /// <summary>
    /// Gets or sets the status message.
    /// </summary>
    public string StatusMessage { get; set; }
}
