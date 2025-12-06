// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for DeletePersonalDataRequest.
/// </summary>
public class DeletePersonalDataValidator : AbstractValidator<DeletePersonalDataRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonalDataValidator"/> class.
    /// </summary>
    public DeletePersonalDataValidator()
    {
        RuleFor(request => request.Password).Password();
    }
}
