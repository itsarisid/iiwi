using Microsoft.AspNetCore.Identity;
using System.Data;

namespace iiwi.Domain.Identity;

public class ApplicationUserRole: IdentityUserRole<int>
{
    public int Id { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ApplicationRole Role { get; set; } = null!;
}
