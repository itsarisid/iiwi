using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication;

public class LoginWith2faHandler(
    SignInManager<ApplicationUser> _signInManager,
    ILogger<LoginWith2faHandler> _logger
    ) : IHandler<LoginWith2faRequest, Response>
{    
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
