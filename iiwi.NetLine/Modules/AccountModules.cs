using iiwi.Application;
using iiwi.Application.Authentication;
using iiwi.Common;

namespace iiwi.NetLine.Modules;

public class AccountModules : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        // Map the endpoints to the route group
        var routeGroup = endpoints.MapGroup(string.Empty).WithGroup(Endpoints.Accounts);

        routeGroup.MapPost("/update-profile",
         IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithName("Update Profile")
        .WithSummary("Update Profile")
        .WithDescription("This api can be used for update user profiles")
        .RequireAuthorization()
        .IncludeInOpenApi(); 
        
        
        routeGroup.MapPost("/send-verification-email",
         IResult (IMediator mediator, SendVerificationEmailRequest request) => mediator
        .HandleAsync<SendVerificationEmailRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithName("Send Verification Email")
        .WithSummary("Send Verification Email")
        .WithDescription("This api can be used for update user profiles")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}
