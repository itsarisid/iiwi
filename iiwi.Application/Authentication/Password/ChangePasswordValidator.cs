// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ChangePasswordRequest.
/// </summary>
public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordValidator"/> class.
    /// <summary>
    /// Initializes validator rules for a ChangePasswordRequest.
    /// </summary>
    /// <remarks>
    /// - Ensures OldPassword satisfies the project's password rules.
    /// - Ensures NewPassword satisfies the project's password rules.
    /// - Ensures ConfirmPassword satisfies confirm-password rules and is equal to NewPassword.
    /// <summary>
    /// Initializes a validator that enforces password-related rules for a change-password request.
    /// </summary>
    /// <remarks>
    /// - Validates <c>OldPassword</c> using the standard password rule.
    /// - Validates <c>NewPassword</c> using the standard password rule.
    /// - Validates <c>ConfirmPassword</c> using the confirm-password rule and requires it to be equal to <c>NewPassword</c>.
    /// </remarks>
    public ChangePasswordValidator()
    {
        RuleFor(request => request.OldPassword).Password();
        RuleFor(request => request.NewPassword).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x=>x.NewPassword);
    }
}