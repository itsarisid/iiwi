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

namespace iiwi.Application.Authentication;

public class RegisterHandler(
UserManager<ApplicationUser> userManager,
SignInManager<ApplicationUser> signInManager,
ILogger<RegisterHandler> logger,
IMailService mailService) : IHandler<RegisterRequest, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ILogger<RegisterHandler> _logger = logger;
    private readonly IMailService _mailService = mailService;

    public async Task<Result<RegisterResponse>> HandleAsync(RegisterRequest request)
    {

        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            //var callbackUrl = new { area = "Identity", userId = user.Id, code };

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
