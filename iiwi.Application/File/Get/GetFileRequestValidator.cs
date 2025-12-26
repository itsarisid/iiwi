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
    /// <summary>
/// Initializes a new <see cref="GetFileRequestValidator"/> that enforces the request's <c>Id</c> must be a valid GUID.
/// <summary>
/// Configures validation rules for GetFileRequest, ensuring the request's Id is a valid GUID.
/// </summary>
    public GetFileRequestValidator() => RuleFor(request => request.Id).Guid();
}