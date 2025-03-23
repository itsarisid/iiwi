using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace Architecture.Application.Authentication
{
    public class DownloadPersonalDataHandler(
    UserManager<IdentityUser> userManager,
    IClaimsProvider claimsProvider,
    ILogger<LoginHandler> logger,
    IResultService resultService) : IHandler<DownloadPersonalDataRequest, Response>
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly IClaimsProvider _claimsProvider = claimsProvider;
        private readonly IResultService _resultService = resultService;
        private readonly ILogger<LoginHandler> _logger = logger;

        public async Task<Result<Response>> HandleAsync(DownloadPersonalDataRequest request)
        {
            var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
            if (user == null)
            {
                return _resultService.Error<Response>($"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(_claimsProvider.ClaimsPrinciple));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(IdentityUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            return _resultService.Success<Response>(new DownloadPersonalDataResponse
            {
                Message = "Personal Data",
                Data= personalData
            });
        }
    }
}
