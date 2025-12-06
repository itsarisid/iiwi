
using iiwi.Application.Authentication;
using iiwi.Common.Privileges;
using iiwi.Model.Enums;
using iiwi.Model.Endpoints;
using iiwi.NetLine.APIDoc;
using iiwi.NetLine.Builders;
using iiwi.NetLine.Extensions;
using iiwi.NetLine.Filters;

namespace iiwi.NetLine.API;

/// <summary>
/// Provides endpoints for user account management and personal data operations
/// </summary>
/// <remarks>
/// This module handles all account-related operations including:
/// - Profile information updates
/// - Email management
/// - Personal data operations
/// - Phone number updates
/// All endpoints require user authentication.
/// </remarks>
public class AccountEndpoints : IEndpoint
{
    /// <summary>
    /// Configures all account management endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        // Create an authorized route group for account endpoints
        var routeGroup = app.MapGroup(string.Empty)
            .WithGroup(AccountsDoc.Group)
            .RequireAuthorization()
            .AddEndpointFilter<ExceptionHandlingFilter>();

        /// <summary>
        /// [POST] /accounts/update-profile - Updates user profile information
        /// </summary>
        /// <remarks>
        /// Allows authenticated users to update their profile details including:
        /// - Display name
        /// - Profile picture
        /// - Personal preferences
        /// 
        /// All fields are optional - only provided fields will be updated.
        /// </remarks>
        /// <param name="request">Contains updated profile fields</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Profile updated successfully</response>
        /// <response code="400">Invalid profile data</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<UpdateProfileRequest, Response>
        {
            EndpointDetails = AccountsDoc.UpdateProfile,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.UpdateProfile],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [POST] /accounts/send-verification - Sends email verification
        /// </summary>
        /// <remarks>
        /// Initiates sending of verification details to the user's registered email.
        /// Typically contains:
        /// - Email verification link
        /// - Account security information
        /// - Important notifications
        /// </remarks>
        /// <param name="request">Email sending parameters</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Verification email sent</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<SendVerificationEmailRequest, Response>
        {
            EndpointDetails = AccountsDoc.SendVerificationDetails,
            HttpMethod = HttpVerb.Post,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.SendVerificationDetails],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [GET] /accounts/download-data - Exports personal data
        /// </summary>
        /// <remarks>
        /// Generates and downloads a complete archive of the user's personal data
        /// in compliance with data protection regulations. Includes:
        /// - Profile information
        /// - Activity history
        /// - Connected accounts
        /// - Preferences
        /// 
        /// The data is typically returned as a JSON or ZIP archive.
        /// </remarks>
        /// <returns>Personal data archive</returns>
        /// <response code="200">Returns personal data file</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<DownloadPersonalDataRequest, Response>
        {
            EndpointDetails = AccountsDoc.DownloadPersonalData,
            HttpMethod = HttpVerb.Get,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.DownloadInfo],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [PUT] /accounts/change-email - Updates user email address
        /// </summary>
        /// <remarks>
        /// Changes the account's primary email address. Requires:
        /// 1. Verification of current email
        /// 2. Confirmation of new email
        /// 
        /// A verification email will be sent to the new address.
        /// </remarks>
        /// <param name="request">Contains old and new email addresses</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Email change initiated</response>
        /// <response code="400">Invalid email addresses</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<ChangeEmailRequest, Response>
        {
            EndpointDetails = AccountsDoc.ChangeEmail,
            HttpMethod = HttpVerb.Put,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.ChangeEmail],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [DELETE] /accounts/delete-data - Removes personal data
        /// </summary>
        /// <remarks>
        /// Permanently deletes the user's personal data from the system
        /// in compliance with data protection regulations. This action:
        /// - May be irreversible
        /// - Could disable account functionality
        /// - May require additional verification
        /// </remarks>
        /// <param name="request">Confirmation parameters</param>
        /// <returns>Operation result</returns>
        /// <response code="204">Data deleted successfully</response>
        /// <response code="400">Invalid request</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<DeletePersonalDataRequest, Response>
        {
            EndpointDetails = AccountsDoc.DeletePersonalData,
            HttpMethod = HttpVerb.Delete,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.DeletePersonalData],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });

        /// <summary>
        /// [PUT] /accounts/update-phone - Changes phone number
        /// </summary>
        /// <remarks>
        /// Updates the user's verified phone number. Requires:
        /// 1. Verification of current number (if exists)
        /// 2. Verification of new number via SMS
        /// </remarks>
        /// <param name="request">Contains phone number details</param>
        /// <returns>Operation result</returns>
        /// <response code="200">Phone number updated</response>
        /// <response code="400">Invalid phone number</response>
        /// <response code="401">Unauthorized request</response>
        routeGroup.MapVersionedEndpoint(new Configure<UpdatePhoneNumberRequest, Response>
        {
            EndpointDetails = AccountsDoc.UpdatePhoneNumber,
            HttpMethod = HttpVerb.Put,
            EnableCaching = true,
            CachePolicy = CachePolicy.NoCache,
            AuthorizationPolicies = [Permissions.Account.UpdatePhoneNumber],
            EnableHttpLogging = true,
            EndpointFilters = ["LoggingFilter"]
        });
    }
}
