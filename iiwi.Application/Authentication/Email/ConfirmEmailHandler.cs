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
    public class ConfirmEmailHandler(
    UserManager<IdentityUser> userManager,
    IResultService resultService) : IHandler<ConfirmEmailRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{request.UserId}'.");
            }

            request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            var result = await _userManager.ConfirmEmailAsync(user, request.Code);
            if (result.Succeeded)
            {
                return _resultService.Success(new Response
                {
                    Message = "Thank you for confirming your email."
                });
            }
            else
            {
                return _resultService.Error<Response>("Error confirming your email.");
            }
        }
    }
}
