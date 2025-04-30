using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

/// <summary>
/// Defines the account-related API endpoints.
/// </summary>
public class AccountModules : IEndpoints
{
    /// <summary>
    /// Configures the account-related API routes.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder to configure routes.</param>
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Accounts.Group);

        /// <summary>
        /// Updates the user's profile information.
        /// </summary>
        routeGroup.MapPost(Accounts.UpdateProfile.Endpoint,
         IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithDocumentation(Accounts.UpdateProfile)
        .RequireAuthorization();

        /// <summary>
        /// Sends verification details to the user's email.
        /// </summary>
        routeGroup.MapPost(Accounts.SendVerificationDetails.Endpoint,
         IResult (IMediator mediator, SendVerificationEmailRequest request) => mediator
        .HandleAsync<SendVerificationEmailRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.SendVerificationDetails)
        .RequireAuthorization();

        /// <summary>
        /// Downloads the user's personal data.
        /// </summary>
        routeGroup.MapGet(Accounts.DownloadPersonalData.Endpoint,
         IResult (IMediator mediator) => mediator
        .HandleAsync<DownloadPersonalDataRequest, Response>(new DownloadPersonalDataRequest())
        .Response())
        .WithDocumentation(Accounts.DownloadPersonalData)
        .RequireAuthorization();

        /// <summary>
        /// Changes the user's email address.
        /// </summary>
        routeGroup.MapPut(Accounts.ChangeEmail.Endpoint,
         IResult (IMediator mediator, ChangeEmailRequest request) => mediator
        .HandleAsync<ChangeEmailRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.ChangeEmail)
        .RequireAuthorization();

        /// <summary>
        /// Deletes the user's personal data.
        /// </summary>
        routeGroup.MapDelete(Accounts.DeletePersonalData.Endpoint,
         IResult (IMediator mediator, [FromBody] DeletePersonalDataRequest request) => mediator
        .HandleAsync<DeletePersonalDataRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.DeletePersonalData)
        .RequireAuthorization();

        /// <summary>
        /// Deletes the user's personal data.
        /// </summary>
        routeGroup.MapPut(Accounts.UpdatePhoneNumber.Endpoint,
         IResult (IMediator mediator, UpdatePhoneNumberRequest request) => mediator
        .HandleAsync<UpdatePhoneNumberRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.UpdatePhoneNumber)
        .RequireAuthorization();

        
    }
}