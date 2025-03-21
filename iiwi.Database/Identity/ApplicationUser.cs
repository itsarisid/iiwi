using Microsoft.AspNetCore.Identity;

namespace iiwi.Database.Identity;

public class ApplicationUser : IdentityUser
{
    public string Address { get; set; }
    public string Gender { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
