using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;
using System.Runtime.InteropServices;

namespace iiwi.AppWire.Controllers;

/// <summary>Test Controller</summary>

[Route("api/v{version:apiVersion}/[controller]")]
public class TestController(IServiceProvider serviceProvider) : BaseController
{
    private readonly IWebHostEnvironment _hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

    /// <summary>Tests this instance.</summary>
    /// <returns>
    ///   <br />
    /// </returns>
    [HttpGet()]
    [AllowAnonymous]
    [DisableRateLimiting]
    public IActionResult Test()
    {
        var assembly = Assembly.GetExecutingAssembly().FullName;
        var result = new
        {
            Auther = "iiwi",
            Version = "1.0.0",
            Assembly = assembly,
            Environment = _hostingEnvironment.EnvironmentName,
            Environment.MachineName,
            Framework = RuntimeInformation.FrameworkDescription,
            OS = $"{RuntimeInformation.OSDescription} - ({RuntimeInformation.OSArchitecture})",
        };
        return Ok(result);
    }
}
