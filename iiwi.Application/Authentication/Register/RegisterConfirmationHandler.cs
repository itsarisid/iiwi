using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
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
    /// Handler for register confirmation.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_mailService">The mail service.</param>
    internal class RegisterConfirmationHandler(
    UserManager<ApplicationUser> _userManager,
    IMailService _mailService) : IHandler<RegisterConfirmationRequest, Response>
    {

        /// <summary>
        /// Handles the register confirmation request asynchronously.
        /// </summary>
        /// <param name="request">The register confirmation request.</param>
        /// <summary>
        /// Processes a registration confirmation request: looks up the user by email, generates and encodes an email confirmation token, sends a confirmation email, and returns an HTTP result indicating success or failure.
        /// </summary>
        /// <param name="request">The register confirmation request containing the email to confirm.</param>
        /// <summary>
        /// Processes a register confirmation request by locating the user, generating an email confirmation token, and sending a confirmation email.
        /// </summary>
        /// <param name="request">The register confirmation request containing the email address to confirm.</param>
        /// <returns>`Result<Response>` with `HttpStatusCode.BadRequest` and a response message if no user matches the provided email; otherwise `HttpStatusCode.OK` with a response prompting the user to check their email for confirmation.</returns>
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