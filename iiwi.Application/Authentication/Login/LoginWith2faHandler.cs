using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class LoginWith2faHandler(
        SignInManager<IdentityUser> signInManager,
        IResultService resultService,
        ILogger<LoginWith2faHandler> logger
        ) : IHandler<LoginWith2faRequest, Response>
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LoginWith2faHandler> _logger = logger;

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
                return _resultService.Success<Response>(new RegisterResponse
                {
                    Message = $"User with ID '{user.Id}' logged in with 2fa.",
                });
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return _resultService.Error<Response>($"User with ID '{user.Id}' account locked out.");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                return _resultService.Error<Response>("Invalid authenticator code.");
            }
        }
    }
}
