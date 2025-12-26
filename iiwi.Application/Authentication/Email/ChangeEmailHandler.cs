using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication.Email;

public class ChangeEmailHandler(
    UserManager<ApplicationUser> _userManager,
    IClaimsProvider _claimsProvider,
    IMailService _mailService) : IHandler<ChangeEmailRequest, Response>
{
    /// <summary>
    /// Initiates an email change for the current user by sending a confirmation link to the requested address if it differs from the current email.
    /// </summary>
    /// <param name="request">Request containing the new email address to confirm.</param>
    /// <returns>A Result wrapping a Response:
    /// <summary>
    /// Handles a request to change the current user's email and, when different, sends a confirmation link to the new address.
    /// </summary>
    /// <param name="request">The change-email request containing the desired new email address.</param>
    /// <returns>The result containing an HTTP status and response message: `404 NotFound` if the current user cannot be loaded; `200 OK` with a confirmation message when a change-email link is sent to the new address; `200 OK` with a message that the email is unchanged when the new email matches the current address.</returns>
    public async Task<Result<Response>> HandleAsync(ChangeEmailRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.NotFound, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            });
        }

        var email = await _userManager.GetEmailAsync(user);
        if (request.NewEmail != email)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);

            await _mailService.SendEmailWithTemplateAsync(new EmailSettings
            {
                Emails = [request.NewEmail],
                Subject = "Confirm your email",
                Model = new
                {
                    FirstName = "Sajid",
                    LastName = "Khan"
                },
                TemplateName = Templates.ChangeEmail
            });

            return new Result<Response>(HttpStatusCode.OK, new Response
            {
                Message = "Confirmation link to change email sent. Please check your email."
            });
        }
        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Your email is unchanged."
        });
    }
}