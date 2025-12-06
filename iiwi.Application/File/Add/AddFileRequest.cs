using DotNetCore.Objects;

namespace iiwi.Application.File;

/// <summary>
/// Request model for adding files.
/// </summary>
/// <param name="Files">The files to add.</param>
public sealed record AddFileRequest(IEnumerable<BinaryFile> Files);
