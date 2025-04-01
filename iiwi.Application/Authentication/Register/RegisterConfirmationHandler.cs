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
internal class RegisterConfirmationHandler(
UserManager<ApplicationUser> _userManager,
IMailService _mailService) : IHandler<RegisterConfirmationRequest, Response>
{

    /// <summary>
    ///  Function Name :  HandleAsync.
    /// </summary>
    /// <param name="request">This request's Datatype is : iiwi.Application.Authentication.RegisterConfirmationRequest.</param>
    /// <returns>System.Threading.Tasks.Task<DotNetCore.Results.Result<iiwi.Application.Response>>.</returns>
    public async Task<Result<Response>> HandleAsync(RegisterConfirmationRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with email '{request.Email}'."
            });
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
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
            TemplateName = Templates.ConfirmEmail
        });

        return new Result<Response>(HttpStatusCode.OK, new Response
        {
            Message = "Please Check your email for confirmation"
        });
    }
}
