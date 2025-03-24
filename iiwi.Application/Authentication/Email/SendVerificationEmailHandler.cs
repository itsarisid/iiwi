using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Domain.Identity;
using iiwi.Infrastructure;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text;

namespace iiwi.Application.Authentication
{
    public class SendVerificationEmailHandler(
    UserManager<ApplicationUser> userManager,
    IClaimsProvider claimsProvider,
    IMailService mailService) : IHandler<SendVerificationEmailRequest, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IMailService _mailService = mailService;

        public async Task<Result<Response>> HandleAsync(SendVerificationEmailRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return new Result<Response>(HttpStatusCode.BadRequest, new Response
                {
                    Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
                });
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var callbackUrl = new { area = "Identity", userId = userId, code = code },

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
}
