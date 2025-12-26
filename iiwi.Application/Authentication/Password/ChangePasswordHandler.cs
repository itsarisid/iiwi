using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
    /// Handler for changing the user's password.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_signInManager">The sign-in manager.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    /// <param name="_logger">The logger.</param>
    public class ChangePasswordHandler(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IClaimsProvider _claimsProvider,
    ILogger<ChangePasswordHandler> _logger) : IHandler<ChangePasswordRequest, Response>
    {

        /// <summary>
        /// Handles the change password request asynchronously.
        /// </summary>
        /// <param name="request">The change password request.</param>
        /// <summary>
        /// Handles a change-password request for the current user.
        /// </summary>
        /// <param name="request">Contains the current (old) and new passwords for the change operation.</param>
        /// <returns>
        /// A Result containing a Response:
        /// - HTTP 400 (BadRequest) with a message when the current user cannot be loaded.
        /// - HTTP 500 (InternalServerError) with a message when the password change fails.
        /// - HTTP 200 (OK) with a message when the password change succeeds.
        /// <summary>
        /// Changes the current user's password using the provided old and new passwords.
        /// </summary>
        /// <param name="request">Request containing the user's current password and the desired new password.</param>
        /// <returns>
        /// A Result containing a Response with an HTTP status code:
        ///  - 400 (BadRequest) if the current user cannot be loaded;
        ///  - 500 (InternalServerError) if the password change fails;
        ///  - 200 (OK) if the password was changed successfully.
        /// The Response.Message contains a human-readable description of the outcome.
        /// </returns>
        public async Task<Result<Response>> HandleAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                _logger.LogError("Error while changing password");
                return new Result<Response>(HttpStatusCode.InternalServerError, new Response
                {
                    Message = "Error while changing password"
                });
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Error while changing password"
            });
        }
    }