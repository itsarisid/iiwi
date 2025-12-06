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

/// <summary>
/// Handler for processing forgot password requests.
/// </summary>
/// <param name="_userManager">The user manager.</param>
/// <param name="_mailService">The mail service.</param>
public class ForgotPasswordHandler(
UserManager<ApplicationUser> _userManager,
IMailService _mailService) : IHandler<ForgotPasswordRequest, Response>
{


    /// <summary>
    /// Handles the forgot password request asynchronously.
    /// </summary>
    /// <param name="request">The forgot password request.</param>
    /// <returns>A result containing the response.</returns>
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
