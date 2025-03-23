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
    public class LoginHandler(
        SignInManager<IdentityUser> signInManager,
        IResultService resultService,
        ILogger<LoginHandler> logger
        ) : IHandler<LoginRequest, Response>
    {
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LoginHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(LoginRequest request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return _resultService.Success<Response>(new RegisterResponse
                {
                    Message = "User logged in.",
                });
            }
            if (result.RequiresTwoFactor)
            {
                _logger.LogInformation("Login with 2fa");
                return _resultService.Success<Response>(new RegisterResponse
                {
                    Message = "Login with 2fa",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return _resultService.Success<Response>(new RegisterResponse
                {
                    Message = "User account locked out.",
                });
            }
            else
            {
                return _resultService.Error<Response>("Invalid login attempt.");
            }
        }
    }
}
