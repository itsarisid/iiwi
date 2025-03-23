using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Architecture.Application.Authentication
{
    public class ExternalLoginsHandler(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IClaimsProvider claimsProvider,
    IResultService resultService) : IHandler<ExternalLoginsRequest, ExternalLoginsResponse>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IResultService _resultService = resultService;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;

        public async Task<Result<ExternalLoginsResponse>> HandleAsync(ExternalLoginsRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<ExternalLoginsResponse>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            var currentLogins = await _userManager.GetLoginsAsync(user);

            var otherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
            .Where(auth => currentLogins.All(ul => auth.Name != ul.LoginProvider))
            .ToList();

            return _resultService.Success(new ExternalLoginsResponse
            {
                CurrentLogins = currentLogins,
                OtherLogins = otherLogins,
                ShowRemoveButton = user.PasswordHash != null || currentLogins.Count > 1
            });
        }
    }
}

