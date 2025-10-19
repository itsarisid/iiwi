using DotNetCore.Mediator;
using DotNetCore.Results;
using iiwi.Application.Account;
using iiwi.Application.Authorization;
using iiwi.Application.Provider;
using iiwi.Database.Permissions;
using iiwi.Model.PingPong;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.PingPong;

public class SystemInfoHandler(
    IWebHostEnvironment hostEnvironment,
    ILogger<UpdateProfileHandler> _logger) : IHandler<EmptyRequest, SystemInfoResponse>
{
    public async Task<Result<SystemInfoResponse>> HandleAsync(EmptyRequest request)
    {
        _logger.LogWarning("Ping pong service called.");
        return new Result<SystemInfoResponse>(HttpStatusCode.OK, new SystemInfoResponse
        {
            MachineName = Environment.MachineName,
            Author = "Sajid Khan",
            Environment = hostEnvironment.EnvironmentName,
            Message = "Ping Pong Information"
        });
    }
}
