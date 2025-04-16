﻿using System.Reflection;
using System.Runtime.InteropServices;

namespace iiwi.NetLine.Modules;

public class TestModule : IEndpoints
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/test",
         IResult (IServiceProvider serviceProvider) => TypedResults.Ok(new
         {
             Auther = "Sajid Khan",
             Version = "1.0.0",
             Date = DateTime.Now.ToLongDateString(),
             Assembly = Assembly.GetExecutingAssembly().FullName,
             Environment = serviceProvider.GetRequiredService<IWebHostEnvironment>().EnvironmentName,
             Environment.MachineName,
             Framework = RuntimeInformation.FrameworkDescription,
             OS = $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})",
         }))
         .WithTags("Test")
         .WithName("Test")
         .WithSummary("Tes end points")
         .WithDescription("This api endpoint can be used for testing")
         .AllowAnonymous()
         .IncludeInOpenApi();
    }
}
