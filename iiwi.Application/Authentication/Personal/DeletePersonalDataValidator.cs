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
    /// <summary>
    /// Initializes a new instance of <see cref="DeletePersonalDataValidator"/> and configures validation rules for <see cref="DeletePersonalDataRequest"/>.
    /// </summary>
    /// <remarks>
    /// Requires the request's <c>Password</c> to satisfy the application's password policy.
    /// <summary>
    /// Configures validation rules for DeletePersonalDataRequest.
    /// </summary>
    /// <remarks>
    /// The Password property must satisfy the application's password policy.
    /// </remarks>
    public DeletePersonalDataValidator()
    {
        RuleFor(request => request.Password).Password();
    }
}