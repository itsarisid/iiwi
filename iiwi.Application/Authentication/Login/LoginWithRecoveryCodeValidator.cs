// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for LoginWithRecoveryCodeRequest.
/// </summary>
public class LoginWithRecoveryCodeValidator : AbstractValidator<LoginWithRecoveryCodeRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWithRecoveryCodeValidator"/> class.
    /// </summary>
    public LoginWithRecoveryCodeValidator()
    {
        RuleFor(request => request.RecoveryCode).NotEmpty();
    }
}
