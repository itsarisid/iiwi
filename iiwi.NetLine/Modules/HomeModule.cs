using Carter.OpenApi;
using DotNetCore.Mediator;
using iiwi.Application;
using iiwi.Application.Account;
using iiwi.NetLine.Config;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/home", (IMediator mediator, UpdateProfileRequest request, Response response) =>mediator
        .HandleAsync<UpdateProfileRequest, Response>(request)
        .MyResult())
        .WithTags("Home")
        .WithName("Index")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}