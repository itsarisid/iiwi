using DotNetCore.Objects;

namespace iiwi.Application.File;

/// <summary>
/// Executes the AddFileRequest operation.
/// </summary>
public sealed record AddFileRequest(IEnumerable<BinaryFile> Files);
