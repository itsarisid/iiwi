using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class LoginHandler(
        SignInManager<ApplicationUser> _signInManager,
        ILogger<ApplicationUser> _logger
        ) : IHandler<LoginRequest, Response>
    {        
        public async Task<Result<Response>> HandleAsync(LoginRequest request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return new Result<Response>(HttpStatusCode.OK, new Response
                {
                    Message = "User logged in."
                });
            }
            if (result.RequiresTwoFactor)
            {
                _logger.LogInformation("Login with 2fa");
                return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "Login with 2fa",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return new Result<Response>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "User account locked out.",
                });
            }
            else
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new RegisterResponse
                {
                    Message = "Invalid login attempt."
                });
            }
        }
    }
}
