using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Application.Authentication
{
    public class SetPasswordHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    ILogger<SetPasswordHandler> logger,
    IResultService resultService) : IHandler<SetPasswordRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<SetPasswordHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(SetPasswordRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                //Note: goto change password
                return _resultService.Error<Response>("Password already set");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                _logger.LogError("Error while setting password");
                return _resultService.Error<Response>("Error while setting password");
            }

            await _signInManager.RefreshSignInAsync(user);
            return _resultService.Success(new Response
            {
                Message = "Your password has been set.",
            });
        }
    }
}
