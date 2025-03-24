using DotNetCore.Mediator;
using DotNetCore.Results;
using iiw.Application.Authentication;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
namespace iiwi.Application.Authentication;
public class DownloadPersonalDataHandler(
UserManager<ApplicationUser> userManager,
IClaimsProvider claimsProvider,
ILogger<DownloadPersonalDataHandler> loggere) : IHandler<DownloadPersonalDataRequest, Response>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IClaimsProvider _claimsProvider = claimsProvider;
    private readonly ILogger<DownloadPersonalDataHandler> _logger = logger;

    public async Task<Result<Response>> HandleAsync(DownloadPersonalDataRequest request)
    {
        var user = await _userManager.GetUserAsync(_claimsProvider.ClaimsPrinciple);
        if (user == null)
        {
            return new Result<Response>(HttpStatusCode.BadRequest, new Response
            {
                Message = $"Unable to load user with ID '{_userManager.GetUserId(_claimsProvider.ClaimsPrinciple)}'."
            }); 
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

        return new Result<Response>(HttpStatusCode.OK, new DownloadPersonalDataResponse
        {
            Message = "Personal Data",
            Data= personalData
        });
    }
}
