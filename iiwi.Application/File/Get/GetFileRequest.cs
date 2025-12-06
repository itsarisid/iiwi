namespace iiwi.Application.File;
/// <summary>
/// Request model for getting a file.
/// </summary>
/// <param name="Id">The file ID.</param>
public sealed record GetFileRequest(Guid Id);
