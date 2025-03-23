using Microsoft.AspNetCore.Identity;

namespace iiwi.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string Gender { get; set; }
    [PersonalData]
    public string FirstName { get; set; }
    [PersonalData]
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public DateTime LastLogin { get; set; }
}
