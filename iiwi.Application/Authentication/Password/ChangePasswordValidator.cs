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
    /// </remarks>
    public ChangePasswordValidator()
    {
        RuleFor(request => request.OldPassword).Password();
        RuleFor(request => request.NewPassword).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x=>x.NewPassword);
    }
}