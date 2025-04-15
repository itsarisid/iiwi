using Carter.OpenApi;
using DotNetCore.Mediator;
using iiwi.Application;
using iiwi.NetLine.Config;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/home", IResult (IMediator mediator, UpdateProfileRequest request) => mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .Response())
        .WithMappingBehaviour<Response>()
        .WithTags("Home")
        .WithName("Index")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}