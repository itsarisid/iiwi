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
    /// Handler for processing login with a recovery code.
    /// </summary>
    /// <param name="_signInManager">The sign-in manager.</param>
    /// <param name="_logger">The logger.</param>
    public class LoginWithRecoveryCodeHandler(
        SignInManager<ApplicationUser> _signInManager,
         ILogger<LoginHandler> _logger
        ) : IHandler<LoginWithRecoveryCodeRequest, Response>
    {

        /// <summary>
        /// Handles the login with recovery code request asynchronously.
        /// </summary>
        /// <param name="request">The login with recovery code request.</param>
        /// <summary>
        /// Handles a sign-in attempt using a two-factor recovery code from the provided request.
        /// </summary>
        /// <param name="request">Request containing the recovery code to use for two-factor authentication.</param>
        /// <returns>A Result containing a Response: on success or lockout the result uses HTTP 200 with a descriptive message; on invalid recovery code the result uses HTTP 400 with an error message.</returns>
        /// <summary>
        /// Handles sign-in using the two-factor authentication recovery code supplied in the request.
        /// </summary>
        /// <param name="request">The request containing the recovery code to use for two-factor sign-in.</param>
        /// <returns>
        /// A Result wrapping a Response and HTTP status code:
        /// - HTTP 200 and a success message when the recovery code authenticates the user.
        /// - HTTP 200 and a lockout message when the user is locked out.
        /// - HTTP 400 and an error message when the recovery code is invalid.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the two-factor authentication user cannot be loaded.</exception>
        public async Task<Result<Response>> HandleAsync(LoginWithRecoveryCodeRequest request)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            var recoveryCode = request.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return new Result<Response>(HttpStatusCode.OK, new Response
                {
                    Message = $"User with ID '{user.Id}' logged in with a recovery code.",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return new Result<Response>(HttpStatusCode.OK, new Response
                {
                    Message = $"User with ID '{user.Id}' account locked out."
                });
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = "Invalid recovery code entered."
                });
            }
        }
    }
}