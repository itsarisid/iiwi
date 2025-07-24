using iiwi.Application;
using iiwi.Application.Authentication;
using iiwi.NetLine.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

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
public class AccountModules : IEndpoints
{
    /// <summary>
    /// Configures all account management endpoints
    /// </summary>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoints is null</exception>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Create an authorized route group for account endpoints
        var routeGroup = endpoints
            .MapGroup(string.Empty)
            .WithGroup(Accounts.Group)
            .RequireAuthorization();

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
        routeGroup.MapPost(Accounts.UpdateProfile.Endpoint,
            IResult (IMediator mediator, UpdateProfileRequest request) => mediator
            .HandleAsync<UpdateProfileRequest, Response>(request)
            .Response())
            .WithMappingBehaviour<Response>()
            .WithDocumentation(Accounts.UpdateProfile);

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
        routeGroup.MapPost(Accounts.SendVerificationDetails.Endpoint,
            IResult (IMediator mediator, SendVerificationEmailRequest request) => mediator
            .HandleAsync<SendVerificationEmailRequest, Response>(request)
            .Response())
            .WithDocumentation(Accounts.SendVerificationDetails);

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
        routeGroup.MapGet(Accounts.DownloadPersonalData.Endpoint,
            IResult (IMediator mediator) => mediator
            .HandleAsync<DownloadPersonalDataRequest, Response>(new DownloadPersonalDataRequest())
            .Response())
            .WithDocumentation(Accounts.DownloadPersonalData);

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
        routeGroup.MapPut(Accounts.ChangeEmail.Endpoint,
            IResult (IMediator mediator, ChangeEmailRequest request) => mediator
            .HandleAsync<ChangeEmailRequest, Response>(request)
            .Response())
            .WithDocumentation(Accounts.ChangeEmail);

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
        routeGroup.MapDelete(Accounts.DeletePersonalData.Endpoint,
            IResult (IMediator mediator, [FromBody] DeletePersonalDataRequest request) => mediator
            .HandleAsync<DeletePersonalDataRequest, Response>(request)
            .Response())
            .WithDocumentation(Accounts.DeletePersonalData);

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
        routeGroup.MapPut(Accounts.UpdatePhoneNumber.Endpoint,
            IResult (IMediator mediator, UpdatePhoneNumberRequest request) => mediator
            .HandleAsync<UpdatePhoneNumberRequest, Response>(request)
            .Response())
            .WithDocumentation(Accounts.UpdatePhoneNumber);
    }
}