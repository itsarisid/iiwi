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

public class RegisterHandler(
UserManager<ApplicationUser> _userManager,
SignInManager<ApplicationUser> _signInManager,
ILogger<RegisterHandler> _logger,
IMailService _mailService) : IHandler<RegisterRequest, RegisterResponse>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.RegisterRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Authentication.RegisterResponse>>.</returns>
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
