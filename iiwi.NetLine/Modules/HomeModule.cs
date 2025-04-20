using iiwi.Application;
using iiwi.NetLine.Extentions;

namespace iiwi.NetLine.Modules;

public class HomeModule : IEndpoints
{
    /// <summary>  
    /// Configures the route for the "/home" endpoint.  
    /// </summary>  
    /// <param name="app">The <see cref="IEndpointRouteBuilder"/> used to define the route.</param>  
    /// <remarks>  
    /// This endpoint handles POST requests to update user profiles.  
    /// It uses the <see cref="IMediator"/> to process the <see cref="UpdateProfileRequest"/>  
    /// and returns a <see cref="Response"/>.  
    /// Additional configurations include:  
    /// - Mapping behavior for the response type.  
    /// - Adding OpenAPI metadata such as tags, summary, and description.  
    /// - Requiring authorization for access.  
    /// </remarks>  
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/home",
         IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>() //Note: If you used TypedResults as return type then this method not required  
        .WithTags("Home")
        .WithName("Index")
        .WithSummary("Update Profile")
        .WithDescription("This api can be used for update user profiles")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}