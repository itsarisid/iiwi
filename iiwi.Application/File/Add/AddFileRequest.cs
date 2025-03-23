using DotNetCore.Objects;

namespace iiwi.Application.File;

public sealed record AddFileRequest(IEnumerable<BinaryFile> Files);
