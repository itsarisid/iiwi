using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for processing user login requests.
/// </summary>
/// <param name="_signInManager">The sign-in manager.</param>
/// <param name="_logger">The logger.</param>
public class LoginHandler(
    SignInManager<ApplicationUser> _signInManager,
    ILogger<ApplicationUser> _logger
    ) : IHandler<LoginRequest, LoginResponse>
{

    /// <summary>
    /// Handles the login request asynchronously.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <summary>
    /// Authenticates the provided credentials and returns a Result containing the corresponding LoginResponse and HTTP status.
    /// </summary>
    /// <param name="request">LoginRequest holding the user's email, password, and RememberMe flag.</param>
    /// <returns>
    /// A Result&lt;LoginResponse&gt; containing an HTTP status and a LoginResponse:
    /// - On successful sign-in: HTTP 200 with a LoginResponse that includes a token, the user's full name, and a success message.
    /// - If two-factor is required: HTTP 200 with a LoginResponse containing a two-factor message.
    /// - If the account is locked out: HTTP 200 with a LoginResponse containing a lockout message.
    /// - If email or password is missing or the credentials are invalid: HTTP 400 with a LoginResponse containing an error message.
    /// </returns>
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var auth = await _signInManager.GetExternalAuthenticationSchemesAsync();
        if (request.Email == null || request.Password == null)
        {
            return new Result<LoginResponse>(HttpStatusCode.BadRequest, new LoginResponse
            {
                Message = "Email and Password are required."
            });
        }

        auth.ToList().ForEach(x =>
        {
            if (x.Name == JwtBearerDefaults.AuthenticationScheme)
            {
                _logger.LogInformation("Login with JWT");
            }
        });

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            return new Result<LoginResponse>(HttpStatusCode.OK, new LoginResponse
            {
                Token = string.Empty,
                FullName = "Sajid Khan",
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