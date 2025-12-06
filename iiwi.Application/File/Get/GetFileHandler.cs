using DotNetCore.Mediator;
using DotNetCore.Objects;
using DotNetCore.Results;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.File.
/// </summary>
namespace iiwi.Application.File;

/// <summary>
/// Handler for getting a file.
/// </summary>
public sealed record GetFileHandler : IHandler<GetFileRequest, BinaryFile>
{
    /// <summary>
    /// Handles the get file request asynchronously.
    /// </summary>
    /// <param name="request">The get file request.</param>
    /// <returns>A result containing the binary file.</returns>
    public async Task<Result<BinaryFile>> HandleAsync(GetFileRequest request)
    {
        var file = await BinaryFile.ReadAsync("Files", request.Id);

        return new Result<BinaryFile>(HttpStatusCode.OK, file);
    }
}
