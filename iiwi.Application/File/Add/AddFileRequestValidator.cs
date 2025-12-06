using FluentValidation;

namespace iiwi.Application.File;

/// <summary>
/// Validator for AddFileRequest.
/// </summary>
public sealed class AddFileRequestValidator : AbstractValidator<AddFileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddFileRequestValidator"/> class.
    /// </summary>
    public AddFileRequestValidator() => RuleFor(request => request.Files).Files();
}

