using DotNetCore.Mediator;
using DotNetCore.Objects;
using DotNetCore.Results;
using iiwi.Common;
using System.Net;

namespace iiwi.Application.File;

public sealed record AddFileHandler : IHandler<AddFileRequest, IEnumerable<BinaryFile>>
{
    public async Task<Result<IEnumerable<BinaryFile>>> HandleAsync(AddFileRequest request)
    {
        var files = await request.Files.SaveAsync(General.Files);

        return new Result<IEnumerable<BinaryFile>>(HttpStatusCode.OK, files);
    }
}
