using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace iiwi.Application.Authentication;

/// <summary>
/// Response model for external logins.
/// </summary>
public record ExternalLoginsResponse : Response { 
    /// <summary>
    /// Gets or sets the list of current logins.
    /// </summary>
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    /// <summary>
    /// Gets or sets the list of other available logins.
    /// </summary>
    public IList<AuthenticationScheme> OtherLogins { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the remove button should be shown.
    /// </summary>
    public bool ShowRemoveButton { get; set; }

    /// <summary>
    /// Gets or sets the status message.
    /// </summary>
    public string StatusMessage { get; set; }
}
