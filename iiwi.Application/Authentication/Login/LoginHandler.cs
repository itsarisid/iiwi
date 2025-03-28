using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using DotNetCore.Security;
using System.Text;
using System.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace iiwi.Application.Authentication
{
    public class LoginHandler(
        SignInManager<ApplicationUser> _signInManager,
        UserManager<ApplicationUser> _userManager,
        ILogger<ApplicationUser> _logger
        ) : IHandler<LoginRequest, LoginResponse>
    {
        public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return new Result<LoginResponse>(HttpStatusCode.OK, new LoginResponse
                {
                    Message = "User logged in."
                });
            }
            if (result.RequiresTwoFactor)
            {
                _logger.LogInformation("Login with 2fa");
                return new Result<LoginResponse>(HttpStatusCode.OK, new LoginResponse
                {
                    Message = "Login with 2fa",
                });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return new Result<LoginResponse>(HttpStatusCode.OK, new LoginResponse
                {
                    Message = "User account locked out.",
                });
            }
            else
            {
                return new Result<LoginResponse>(HttpStatusCode.BadRequest, new LoginResponse
                {
                    Message = "Invalid login attempt."
                });
            }
        }
    }
}
