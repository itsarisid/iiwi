﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace iiwi.Domain.Identity;

public class ApplicationUser : IdentityUser<int>
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

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }=[];
}
