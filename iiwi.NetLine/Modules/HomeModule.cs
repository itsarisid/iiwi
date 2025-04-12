using Carter.OpenApi;
using DotNetCore.Mediator;
using iiwi.Application;

namespace iiwi.NetLine.Modules;

public class HomeModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
        app.MapPost("/home", Task (IMediator mediator, UpdateProfileRequest request, HttpResponse response) => 
        {
            var res = mediator.HandleAsync<UpdateProfileRequest, Response>(request);
            //res.StatusCode = 409;
            //return Results.Text("There's no place like 127.0.0.1");
            return res;
        })
        .WithTags("Home")
        .WithName("Index")
        .IncludeInOpenApi();
    }
}
