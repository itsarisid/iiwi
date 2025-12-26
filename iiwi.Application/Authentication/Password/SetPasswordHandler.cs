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
    /// Handler for setting the user's password.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_signInManager">The sign-in manager.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class SetPasswordHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<SetPasswordHandler> _logger) : IHandler<SetPasswordRequest, Response>
    {

        /// <summary>
        /// Handles the set password request asynchronously.
        /// </summary>
        /// <param name="request">The set password request.</param>
        /// <summary>
        /// Sets the current user's password if the user exists and does not already have a password, then refreshes the user's sign-in.
        /// </summary>
        /// <param name="request">Request containing the new password to set.</param>
        /// <returns>A Result containing a Response with an HTTP status code and a message describing success or the error.</returns>
        public async Task<Result<Response>> HandleAsync(SetPasswordRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = "Password already set"
                });
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                _logger.LogError("Error while setting password");
                return new Result<Response>(HttpStatusCode.InternalServerError, new Response
                {
                    Message = "Error while setting password"
                });
            }

            await _signInManager.RefreshSignInAsync(user);
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Your password has been set.",
            });
        }
    }