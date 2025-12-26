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
    /// <summary>
    /// Retrieves the binary file identified by the provided request.
    /// </summary>
    /// <param name="request">The request containing the identifier of the file to retrieve.</param>
    /// <summary>
    /// Retrieves the binary file identified by the request.
    /// </summary>
    /// <param name="request">The request containing the identifier of the file to retrieve.</param>
    /// <returns>A Result&lt;BinaryFile&gt; with HTTP status code 200 (OK) containing the requested file.</returns>
    public async Task<Result<BinaryFile>> HandleAsync(GetFileRequest request)
    {
        var file = await BinaryFile.ReadAsync("Files", request.Id);

        return new Result<BinaryFile>(HttpStatusCode.OK, file);
    }
}