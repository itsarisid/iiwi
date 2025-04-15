using Carter.OpenApi;
using DotNetCore.Mediator;
using iiwi.Application;
using iiwi.NetLine.Config;
using Microsoft.AspNetCore.Mvc;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
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