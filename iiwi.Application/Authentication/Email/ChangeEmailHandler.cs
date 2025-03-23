using Architecture.Common;
using Architecture.Infrastructure;
using Architecture.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace Architecture.Application.Authentication
{
    public class ChangeEmailHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    IMailService mailService,
    IResultService resultService) : IHandler<ChangeEmailRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IMailService _mailService = mailService;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(ChangeEmailRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (request.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
                //var callbackUrl = new { userId, email = request.NewEmail, code };

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

                return _resultService.Success(new Response
                {
                    Message = "Confirmation link to change email sent. Please check your email."
                });
            }
            return _resultService.Error<Response>("Your email is unchanged.");
        }
    }
}
