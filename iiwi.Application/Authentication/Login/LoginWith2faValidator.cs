// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for LoginWith2faRequest.
/// </summary>
internal class LoginWith2faValidator : AbstractValidator<LoginWith2faRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWith2faValidator"/> class.
    /// <summary>
    /// Configures validation rules for <see cref="LoginWith2faRequest"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that the TwoFactorCode value is not empty and that RememberMachine is not null.
    /// <summary>
    /// Configures validation rules for <see cref="LoginWith2faRequest"/> instances.
    /// </summary>
    /// <remarks>
    /// Enforces that <see cref="LoginWith2faRequest.TwoFactorCode"/> is provided (not empty)
    /// and that <see cref="LoginWith2faRequest.RememberMachine"/> is present (not null).
    /// </remarks>
    public LoginWith2faValidator()
    {
        RuleFor(request => request.TwoFactorCode).NotEmpty();
        RuleFor(request => request.RememberMachine).NotNull();
    }
}