using iiwi.Application;
using iiwi.Application.Authentication;

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

        routeGroup.MapPost(Accounts.DownloadPersonalData.Endpoint,
         IResult (IMediator mediator, DownloadPersonalDataRequest request) => mediator
        .HandleAsync<DownloadPersonalDataRequest, Response>(request)
        .Response())
        .WithDocumentation(Accounts.DownloadPersonalData)
        .RequireAuthorization();

    }
}
