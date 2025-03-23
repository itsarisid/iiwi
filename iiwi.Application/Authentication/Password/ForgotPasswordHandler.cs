using Architecture.Common;
using Architecture.Infrastructure;
using Architecture.Infrastructure.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Architecture.Application.Authentication
{
    public class ForgotPasswordHandler(
    UserManager<IdentityUser> userManager,
    IResultService resultService,
    IMailService mailService) : IHandler<ForgotPasswordRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IMailService _mailService = mailService;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return _resultService.Error<Response>("Please Check your email for resetting password");
            }

            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var callbackUrl = new { area = "Identity", code };

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

            return _resultService.Success(new Response
            {
                Message = "Please Check your email for resetting password"
            });
        }
    }
}
