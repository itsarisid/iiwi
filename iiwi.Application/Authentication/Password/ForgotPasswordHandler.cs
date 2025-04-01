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

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

public class ForgotPasswordHandler(
UserManager<ApplicationUser> _userManager,
IMailService _mailService) : IHandler<ForgotPasswordRequest, Response>
{


    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.ForgotPasswordRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = "Please Check your email for resetting password"
            });
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

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
