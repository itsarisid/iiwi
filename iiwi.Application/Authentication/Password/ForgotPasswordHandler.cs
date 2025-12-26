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
    /// <summary>
    /// Handles a forgot-password request by sending a password-reset email when a user with the provided email exists and their email is confirmed.
    /// </summary>
    /// <param name="request">The forgot-password request containing the user's email.</param>
    /// <returns>
    /// A Result containing a Response with a message instructing the user to check their email; the Result status is 200 (OK) if the reset email was sent and 400 (Bad Request) if the user does not exist or the email is unconfirmed.
    /// <summary>
    /// Handles a forgot-password request by sending a password-reset email when the user exists and their email is confirmed.
    /// </summary>
    /// <param name="request">Request containing the email address to which the password-reset email should be sent.</param>
    /// <returns>
    /// A Result wrapping a Response:
    /// - HTTP 200 OK with a message prompting to check email when a reset email was sent.
    /// - HTTP 400 Bad Request with the same message when the user does not exist or the email is not confirmed.
    /// </returns>
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