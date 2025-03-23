using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Architecture.Application.Authentication
{
    public class ResetPasswordHandler(
    UserManager<IdentityUser> userManager,
    IResultService resultService) : IHandler<ResetPasswordRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IResultService _resultService = resultService;

        public async Task<Result<Response>> HandleAsync(ResetPasswordRequest request)
        {
            request.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return _resultService.Error<Response>($"Unable to load user with email '{request.Email}'.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);
            if (result.Succeeded)
            {
                return _resultService.Success(new Response
                {
                    Message = "Your password has been reset"
                });
            }

            return _resultService.Error<Response>("Error...");

        }
    }
}
