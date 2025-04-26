using iiwi.Application;
using iiwi.Application.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

public class AccountModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Accounts.Group);

        routeGroup.MapPost(Accounts.UpdateProfile.Endpoint,
         IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithDocumentation(Accounts.UpdateProfile)
        .RequireAuthorization();

        routeGroup.MapPost(Accounts.SendVerificationDetails.Endpoint,
         IResult (IMediator mediator, SendVerificationEmailRequest request) => mediator
        .HandleAsync<SendVerificationEmailRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.SendVerificationDetails)
        .RequireAuthorization();

        routeGroup.MapGet(Accounts.DownloadPersonalData.Endpoint,
         IResult (IMediator mediator) => mediator
        .HandleAsync<DownloadPersonalDataRequest, Response>(new DownloadPersonalDataRequest())
        .Response())
        .WithDocumentation(Accounts.DownloadPersonalData)
        .RequireAuthorization();

        routeGroup.MapPut(Accounts.ChangeEmail.Endpoint,
         IResult (IMediator mediator, ChangeEmailRequest request) => mediator
        .HandleAsync<ChangeEmailRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.ChangeEmail)
        .RequireAuthorization();

        routeGroup.MapDelete(Accounts.DeletePersonalData.Endpoint,
         IResult (IMediator mediator, [FromBody] DeletePersonalDataRequest request) => mediator
        .HandleAsync<DeletePersonalDataRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.DeletePersonalData)
        .RequireAuthorization();

        routeGroup.MapPost(Accounts.ChangePassword.Endpoint,
         IResult (IMediator mediator, ChangePasswordRequest request) => mediator
        .HandleAsync<ChangePasswordRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.ChangePassword)
        .RequireAuthorization();

    }
}
