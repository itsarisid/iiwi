using Carter.OpenApi;
using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {   app.MapPost("/home", async Task<Result<Response>>
        (IMediator mediator, UpdateProfileRequest request, Result<Response> response) => 
        {
            var res =   await mediator.HandleAsync<UpdateProfileRequest, Response>(request);

            return TypedResults.Ok(new Response
            {
                Message=res.Message,

            });

            //return TypedResults.Ok<Response>(res);
        })
        .WithTags("Home")
        .WithName("Index")
        .RequireAuthorization()
        .IncludeInOpenApi();
    }
}
