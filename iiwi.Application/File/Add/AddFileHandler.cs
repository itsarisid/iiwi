using DotNetCore.Mediator;
using DotNetCore.Objects;
using DotNetCore.Results;
using iiwi.Common;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.File.
/// </summary>
namespace iiwi.Application.File;

public sealed record AddFileHandler : IHandler<AddFileRequest, IEnumerable<BinaryFile>>
{
    public async Task<Result<IEnumerable<BinaryFile>>> HandleAsync(AddFileRequest request)
    {
        var files = await request.Files.SaveAsync(General.Directories.Files);

        return new Result<IEnumerable<BinaryFile>>(HttpStatusCode.OK, files);
    }
}
