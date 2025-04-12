using Carter.OpenApi;
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {   app.MapPost("/home",  Task<Result<Response>>
        (IMediator mediator, UpdateProfileRequest request, Result<Response> response) => 
        {
            var res =   mediator.HandleAsync<UpdateProfileRequest, Response>(request).ApiResult();


            return TypedResults.Ok<Response>(res);
        })
        .WithTags("Home")
        .WithName("Index")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}
