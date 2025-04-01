using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

public class ChangeEmailHandler(
UserManager<ApplicationUser> _userManager,
IClaimsProvider _claimsProvider,
IMailService _mailService) : IHandler<ChangeEmailRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.ChangeEmailRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(ChangeEmailRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
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
