using DotNetCore.Mediator;
using DotNetCore.Results;

using iiwi.Model.PingPong;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;

namespace iiwi.Application.PingPong;

/// <summary>
/// Handler for getting system information.
/// </summary>
/// <param name="hostEnvironment">The host environment.</param>
/// <param name="_logger">The logger.</param>
public class SystemInfoHandler(
    IWebHostEnvironment hostEnvironment,
    ILogger<SystemInfoHandler> _logger) : IHandler<EmptyRequest, SystemInfoResponse>
{
    /// <summary>
    /// Handles the system info request asynchronously.
    /// </summary>
    /// <param name="request">The empty request.</param>
    /// <summary>
    /// Provides basic system information: machine name, author, hosting environment name, and a message.
    /// </summary>
    /// <param name="request">Ignored; present only to satisfy the handler contract.</param>
    /// <returns>A Result containing a SystemInfoResponse with machine name, author, environment, and a message.</returns>
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