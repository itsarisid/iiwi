using iiwi.Application;
using iiwi.Application.Authentication;

namespace iiwi.NetLine.Modules;

public class AccountModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(EndpointGroup.Accounts);

        routeGroup.MapPost(Endpoints.UpdateProfile.Endpoint,
         IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithEndpointsGroup(Endpoints.UpdateProfile)
        .RequireAuthorization()
        .IncludeInOpenApi(); 
        
        
        routeGroup.MapPost("/send-verification-email",
         IResult (IMediator mediator, SendVerificationEmailRequest request) => mediator
        .HandleAsync<SendVerificationEmailRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithEndpointsGroup(Endpoints.SendVerificationDetails)
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}
