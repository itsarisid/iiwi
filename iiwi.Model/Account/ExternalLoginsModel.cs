
using Microsoft.AspNetCore.Identity;

namespace iiwi.Model.Account;

public class ExternalLoginsModel<T>
{
    public IList<UserLoginInfo> CurrentLogins { get; set; }

    public IList<T> OtherLogins { get; set; }

    public bool ShowRemoveButton { get; set; }
    public string StatusMessage { get; set; }
}
