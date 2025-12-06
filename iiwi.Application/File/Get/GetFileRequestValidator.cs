using FluentValidation;
using iiwi.Application.File;

namespace iiwi.Application.File;

/// <summary>
/// Validator for GetFileRequest.
/// </summary>
public sealed class GetFileRequestValidator : AbstractValidator<GetFileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetFileRequestValidator"/> class.
    /// </summary>
    public GetFileRequestValidator() => RuleFor(request => request.Id).Guid();
}

