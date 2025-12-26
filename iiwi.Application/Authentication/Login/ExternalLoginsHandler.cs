using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication.Login;

public class ExternalLoginsHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider) : IHandler<ExternalLoginsRequest, ExternalLoginsResponse>
{
    /// <summary>
    /// Retrieves the authenticated user's current external login providers and the available external authentication schemes not already linked to the user.
    /// </summary>
    /// <returns>
    /// A Result&lt;ExternalLoginsResponse&gt; with:
    /// - CurrentLogins: the user's configured external logins,
    /// - OtherLogins: external authentication schemes not currently linked,
    /// - ShowRemoveButton: true if the user has a password or more than one login.
    /// Returns a 404 Result with a message if the user cannot be found.
    /// </returns>
    public async Task<Result<ExternalLoginsResponse>> HandleAsync(ExternalLoginsRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<ExternalLoginsResponse>(HttpStatusCode.NotFound, new ExternalLoginsResponse
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