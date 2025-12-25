using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for resetting the authenticator key.
/// </summary>
/// <param name="_userManager">The user manager.</param>
/// <param name="_signInManager">The sign-in manager.</param>
/// <param name="_claimsProvider">The claims provider.</param>
/// <param name="_logger">The logger.</param>
public class ResetAuthenticatorHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager,
IClaimsProvider _claimsProvider,
ILogger<ResetAuthenticatorHandler> _logger) : IHandler<ResetAuthenticatorRequest, Response>
{

    /// <summary>
    /// Handles the reset authenticator request asynchronously.
    /// </summary>
    /// <param name="request">The reset authenticator request.</param>
    /// <summary>
    /// Resets the current user's authenticator app key and disables two-factor authentication for that account.
    /// </summary>
    /// <param name="request">The reset request (contains any request-specific data).</param>
    /// <returns>
    /// A Result containing a Response: a BadRequest response with an error message if the current user cannot be loaded;
    /// otherwise an OK response with a message indicating the authenticator key was reset and reconfiguration is required.
    /// </returns>
    public async Task<Result<Response>> HandleAsync(ResetAuthenticatorRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        await _userManager.SetTwoFactorEnabledAsync(user, false);
        await _userManager.ResetAuthenticatorKeyAsync(user);
        _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

        await _signInManager.RefreshSignInAsync(user);
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.",
        });
    }
}