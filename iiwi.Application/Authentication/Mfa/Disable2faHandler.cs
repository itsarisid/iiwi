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
namespace iiwi.Application.Authentication
{
    /// <summary>
    /// Handler for disabling two-factor authentication.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class Disable2faHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    ILogger<Disable2faHandler> _logger) : IHandler<Disable2faRequest, Response>
    {

        /// <summary>
        /// Handles the disable 2FA request asynchronously.
        /// </summary>
        /// <param name="request">The disable 2FA request.</param>
        /// <summary>
        /// Disables two-factor authentication for the current user.
        /// </summary>
        /// <param name="request">The request containing any parameters required to disable 2FA.</param>
        /// <returns>
        /// A Result containing a Response:
        /// - On success: HTTP 200 with a message indicating 2FA has been disabled.
        /// - If the current user cannot be loaded: HTTP 400 with an error message identifying the user ID.
        /// </returns>
        /// <summary>
        /// Disables two-factor authentication for the current user.
        /// </summary>
        /// <returns>
        /// A Result containing a Response and an HTTP status:
        /// 200 OK when 2FA was successfully disabled; 400 Bad Request if the current user cannot be loaded.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if disabling two-factor authentication fails for the current user.</exception>
        public async Task<Result<Response>> HandleAsync(Disable2faRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(_claimsProvider.ClaimsPrinciple));
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "2fa has been disabled. You can re enable 2fa when you setup an authenticator app and call TwoFactorAuthentication",
            });
        }
    }
}