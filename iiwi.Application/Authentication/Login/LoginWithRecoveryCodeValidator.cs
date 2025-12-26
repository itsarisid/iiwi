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
    /// <summary>
    /// Initializes a validator that ensures a login request contains a recovery code.
    /// </summary>
    /// <remarks>
    /// Adds a rule requiring the request's <c>RecoveryCode</c> property to be non-empty.
    /// <summary>
    /// Initializes a validator that ensures a login request contains a recovery code.
    /// </summary>
    /// <remarks>
    /// Adds a validation rule requiring the `RecoveryCode` property to be present and not empty.
    /// </remarks>
    public LoginWithRecoveryCodeValidator()
    {
        RuleFor(request => request.RecoveryCode).NotEmpty();
    }
}