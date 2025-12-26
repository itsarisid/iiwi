using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for processing two-factor authentication login.
/// </summary>
/// <param name="_signInManager">The sign-in manager.</param>
/// <param name="_logger">The logger.</param>
public class LoginWith2faHandler(
    SignInManager<ApplicationUser> _signInManager,
    ILogger<LoginWith2faHandler> _logger
    ) : IHandler<LoginWith2faRequest, Response>
{

    /// <summary>
    /// Handles the two-factor authentication login request asynchronously.
    /// </summary>
    /// <param name="request">The login with 2FA request.</param>
    /// <summary>
    /// Handles a two-factor authentication sign-in request and returns the resulting response.
    /// </summary>
    /// <param name="request">The two-factor authentication request containing the authenticator code and machine remember flags.</param>
    /// <returns>A Result&lt;Response&gt; containing an HTTP status and a RegisterResponse message indicating success, account lockout, or invalid authenticator code.</returns>
    /// <summary>
    /// Processes a two-factor authentication sign-in request and returns the authentication result.
    /// </summary>
    /// <param name="request">The two-factor sign-in request containing the authenticator code and remember flags.</param>
    /// <returns>
    /// A <see cref="Result{Response}"/> whose payload is a <see cref="RegisterResponse"/>:
    /// - HTTP 200 with a message confirming successful 2FA sign-in when authentication succeeds.
    /// - HTTP 200 with a message indicating account lockout when the user is locked out.
    /// - HTTP 400 with a message indicating an invalid authenticator code for other failures.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the two-factor authentication user cannot be loaded.</exception>
    public async Task<Result<Response>> HandleAsync(LoginWith2faRequest request)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync() ??
            throw new InvalidOperationException($"Unable to load two-factor authentication user.");
        bool rememberMe = request.RememberMachine;

        var authenticatorCode = request.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, request.RememberMachine);

        if (result.Succeeded)
        {
            _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
            return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
            {
                Message = $"User with ID '{user.Id}' logged in with 2fa.",
            });
        }
        else if (result.IsLockedOut)
        {
            _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
            return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
            {
                Message = $"User with ID '{user.Id}' account locked out."
            });
        }
        else
        {
            _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
            return new Result<Response>(HttpStatusCode.BadRequest, new RegisterResponse
            {
                Message = "Invalid authenticator code."
            });
        }
    }
}