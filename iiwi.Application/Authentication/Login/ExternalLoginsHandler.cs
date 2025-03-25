using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication;

public class ExternalLoginsHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager,
IClaimsProvider _claimsProvider) : IHandler<ExternalLoginsRequest, ExternalLoginsResponse>
{
    public async Task<Result<ExternalLoginsResponse>> HandleAsync(ExternalLoginsRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<ExternalLoginsResponse>(HttpStatusCode.BadRequest, new ExternalLoginsResponse
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var currentLogins = await _userManager.GetLoginsAsync(user);

        var otherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
        .Where(auth => currentLogins.All(ul => auth.Name != ul.LoginProvider))
        .ToList();

        return new Result<ExternalLoginsResponse>(HttpStatusCode.OK, new ExternalLoginsResponse
        {
            CurrentLogins = currentLogins,
            OtherLogins = otherLogins,
            ShowRemoveButton = user.PasswordHash != null || currentLogins.Count > 1
        });
    }
}

