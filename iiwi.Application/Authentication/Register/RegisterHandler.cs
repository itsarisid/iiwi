using iiwi.Common;
using DotNetCore.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;
using DotNetCore.Mediator;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using System.Net;
using iiwi.Domain.Identity;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

/// <summary>
/// Handler for user registration.
/// </summary>
/// <param name="_userManager">The user manager.</param>
/// <param name="_signInManager">The sign-in manager.</param>
/// <param name="_logger">The logger.</param>
/// <param name="_mailService">The mail service.</param>
public class RegisterHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager,
ILogger<RegisterHandler> _logger,
IMailService _mailService) : IHandler<RegisterRequest, RegisterResponse>
{

    /// <summary>
    /// Handles the register request asynchronously.
    /// </summary>
    /// <param name="request">The register request.</param>
    /// <summary>
    /// Processes a registration request by creating a new user, sending an email confirmation, and signing the user in when account confirmation is not required.
    /// </summary>
    /// <param name="request">Registration details including the user's email and desired password.</param>
    /// <returns>
    /// A Result containing a RegisterResponse and an HTTP status:
    /// - HTTP 200 OK with a message indicating successful account creation (or instructing the user to confirm their email when confirmation is required).
    /// - HTTP 500 InternalServerError with a generic error message if user creation fails.
    /// <summary>
    /// Handles user registration: creates a new account, sends an email confirmation, and signs in the user when account confirmation is not required.
    /// </summary>
    /// <param name="request">Registration data containing the user's email and password.</param>
    /// <returns>
    /// A Result&lt;RegisterResponse&gt; containing:
    /// - HTTP 200 with a message prompting email confirmation if account confirmation is required.
    /// - HTTP 200 with a success message after signing in if confirmation is not required.
    /// - HTTP 500 with a generic error message if account creation fails.
    /// </returns>
    public async Task<Result<RegisterResponse>> HandleAsync(RegisterRequest request)
    {

        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            await _mailService.SendEmailWithTemplateAsync(new EmailSettings
            {
                Emails = [request.Email],
                Subject = "Confirm your email",
                Model = new
                {
                    FirstName = "Sajid",
                    LastName = "Khan"
                },
                TemplateName = Templates.Register
            });

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                return new Result<RegisterResponse>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "User created a new account with password. Please Confirm email address.",
                });
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new Result<RegisterResponse>(HttpStatusCode.OK, new RegisterResponse
                {
                    Message = "User created a new account with password.",
                });
            }
        }
        else
        {
            return new Result<RegisterResponse>(HttpStatusCode.InternalServerError, "Error...");
        }
    }
}