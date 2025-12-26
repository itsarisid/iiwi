using DotNetCore.Mediator;
using DotNetCore.Results;

using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Authentication;
using iiwi.Application.Provider;
using iiwi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;
/// <summary>
///       Namespace Name - iiwi.Application.Authentication.
/// </summary>
namespace iiwi.Application.Authentication;

    /// <summary>
    /// Handler for downloading personal data.
    /// </summary>
    /// <param name="_userManager">The user manager.</param>
    /// <param name="_logger">The logger.</param>
    /// <param name="_claimsProvider">The claims provider.</param>
    public class DownloadPersonalDataHandler(
    UserManager<ApplicationUser> _userManager,
    ILogger<DownloadPersonalDataHandler> _logger,
    IClaimsProvider _claimsProvider
    ) : IHandler<DownloadPersonalDataRequest, Response>
    {

        /// <summary>
        /// Handles the download personal data request asynchronously.
        /// </summary>
        /// <param name="request">The download personal data request.</param>
        /// <summary>
        /// Handles a request to retrieve the current user's personal data.
        /// </summary>
        /// <param name="request">The request to download personal data.</param>
        /// <returns>
        /// A Result containing a Response:
        /// - On success: HTTP 200 with a DownloadPersonalDataResponse whose Data is a dictionary of IdentityUser properties marked with PersonalDataAttribute mapped to their string values (or "null") and Message "Personal Data".
        /// - On failure: HTTP 400 with a Response containing an error Message indicating the user could not be loaded.
        /// </returns>
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
                Data = personalData
            });
        }
    }