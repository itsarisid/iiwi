using Architecture.Common;
using Architecture.Domain;
using Architecture.Infrastructure;
using Lucene.Net.Codecs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class ConfirmEmailChangeHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IResultService resultService) : IHandler<ConfirmEmailChangeRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IResultService _resultService = resultService;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        public async Task<Result<Response>> HandleAsync(ConfirmEmailChangeRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{request.UserId}'.");
            }

            request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await _userManager.ChangeEmailAsync(user, request.Email, request.Code);
            if (!result.Succeeded)
            {
                return _resultService.Error<Response>("Error changing email.");
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await _userManager.SetUserNameAsync(user, request.Email);
            if (!setUserNameResult.Succeeded)
            {
                return _resultService.Error<Response>("Error changing user name.");
            }

            await _signInManager.RefreshSignInAsync(user);

            return _resultService.Success(new Response
            {
                Message = "Thank you for confirming your email change."
            });
        }
    }
}
