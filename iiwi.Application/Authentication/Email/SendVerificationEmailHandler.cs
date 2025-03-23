using Architecture.Common;
using Architecture.Infrastructure;
using Architecture.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Architecture.Application.Authentication
{
    public class SendVerificationEmailHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    IMailService mailService,
    IResultService resultService) : IHandler<SendVerificationEmailRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IMailService _mailService = mailService;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(SendVerificationEmailRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
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

            return _resultService.Success(new Response
            {
                Message = "Verification email sent. Please check your email."
            });
        }
    }
}
