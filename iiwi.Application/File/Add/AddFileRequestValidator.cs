using FluentValidation;

namespace iiwi.Application.File;

/// <summary>
/// Validator for AddFileRequest.
/// </summary>
public sealed class AddFileRequestValidator : AbstractValidator<AddFileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddFileRequestValidator"/> class.
    /// <summary>
/// Initializes a validator for <see cref="AddFileRequest"/>, registering file validation rules for its <c>Files</c> property.
/// </summary>
    public AddFileRequestValidator() => RuleFor(request => request.Files).Files();
}
