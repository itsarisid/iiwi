﻿using iiwi.Application;

namespace iiwi.NetLine.Modules;

public class HomeModule : IEndpoints
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