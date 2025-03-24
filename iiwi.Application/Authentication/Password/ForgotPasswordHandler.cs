using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace iiwi.Application.Authentication;

public class ForgotPasswordHandler(
UserManager<ApplicationUser> userManager,
IMailService mailService) : IHandler<ForgotPasswordRequest, Response>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IMailService _mailService = mailService;

    public async Task<Result<Response>> HandleAsync(ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = "Please Check your email for resetting password"
            });
        }

        // For more information on how to enable account confirmation and password reset please 
        // visit https://go.microsoft.com/fwlink/?LinkID=532713
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //var callbackUrl = new { area = "Identity", code };

        await _mailService.SendEmailWithTemplateAsync(new EmailSettings
        {
            Emails = [request.Email],
            Subject = "Reset Email",
            Model = new
            {
                FirstName = "Sajid",
                LastName = "Khan"
            },
            TemplateName = Templates.ResetPassword
        });

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Please Check your email for resetting password"
        });
    }
}
