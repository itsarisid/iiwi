using DotNetCore.Mediator;
using DotNetCore.Objects;
using DotNetCore.Results;
using System.Net;

/// <summary>
///       Namespace Name - iiwi.Application.File.
/// </summary>
namespace iiwi.Application.File;

public sealed record GetFileHandler : IHandler<GetFileRequest, BinaryFile>
{
    public async Task<Result<BinaryFile>> HandleAsync(GetFileRequest request)
    {
        var file = await BinaryFile.ReadAsync("Files", request.Id);

        return new Result<BinaryFile>(HttpStatusCode.OK, file);
    }
}
