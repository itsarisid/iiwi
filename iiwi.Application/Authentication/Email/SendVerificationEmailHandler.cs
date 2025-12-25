using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace iiwi.Application.Authentication.Email;

public class SendVerificationEmailHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    IMailService _mailService) : IHandler<SendVerificationEmailRequest, Response>
{
    /// <summary>
    /// Sends an email verification message to the currently authenticated user's email address.
    /// </summary>
    /// <param name="request">The request payload for sending the verification email.</param>
    /// <returns>
    /// A Result containing a Response:
    /// - If the current user cannot be loaded: a NotFound status and a message identifying the user ID that could not be loaded.
    /// - If the email is sent: an OK status and a message indicating the verification email was sent.
    /// </returns>
    public async Task<Result<Response>> HandleAsync(SendVerificationEmailRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var email = await _userManager.GetEmailAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        await _mailService.SendEmailWithTemplateAsync(new EmailSettings
        {
            Emails = [email],
            Subject = "Confirm your email",
            Model = new
            {
                FirstName = "Sajid",
                LastName = "Khan"
            },
            TemplateName = Templates.VerificationEmail
        });

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Verification email sent. Please check your email."
        });
    }
}