using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Provider;
using iiwi.Common;
using iiwi.Infrastructure.Email;
using iiwi.Model.Settings;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace iiwi.Application.Authentication
{
    public class ChangeEmailHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    IMailService mailService) : IHandler<ChangeEmailRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IMailService _mailService = mailService;

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
}
