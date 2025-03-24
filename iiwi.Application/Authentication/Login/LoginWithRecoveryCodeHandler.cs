
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class LoginWithRecoveryCodeHandler(
        SignInManager<ApplicationUser> signInManager,
         ILogger<LoginHandler> logger
        ) : IHandler<LoginWithRecoveryCodeRequest , Response>
    {
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ILogger<LoginHandler> _logger = logger;

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
