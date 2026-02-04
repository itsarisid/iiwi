using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace iiwi.Application.Authentication;

public record ExternalLoginsResponse : Response { 
    /// <summary>
    /// Gets or sets the CurrentLogins.
    /// </summary>
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    /// <summary>
    /// Gets or sets the OtherLogins.
    /// </summary>
    public IList<AuthenticationScheme> OtherLogins { get; set; }

    /// <summary>
    /// Gets or sets the ShowRemoveButton.
    /// </summary>
    public bool ShowRemoveButton { get; set; }
    /// <summary>
    /// Gets or sets the StatusMessage.
    /// </summary>
    public string StatusMessage { get; set; }
}
