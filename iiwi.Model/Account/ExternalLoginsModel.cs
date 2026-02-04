
using Microsoft.AspNet.Identity;

namespace iiwi.Model.Account;

public class ExternalLoginsModel<T>
{
    /// <summary>
    /// Gets or sets the CurrentLogins.
    /// </summary>
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    /// <summary>
    /// Gets or sets the OtherLogins.
    /// </summary>
    public IList<T> OtherLogins { get; set; }

    /// <summary>
    /// Gets or sets the ShowRemoveButton.
    /// </summary>
    public bool ShowRemoveButton { get; set; }
    /// <summary>
    /// Gets or sets the StatusMessage.
    /// </summary>
    public string StatusMessage { get; set; }
}
