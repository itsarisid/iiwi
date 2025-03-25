using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iiwi.Application.Account;

public record UpdateProfileRequest
{
    public string Gender { get; set; }
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Display(Name = "Display Name")]
    public string DisplayName { get; set; }
    [Display(Name = "Date of Birth")]
    public DateTime DOB { get; set; }
    [Display(Name = "Address")]
    public string Address { get; set; }
}