// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ResetPasswordRequest.
/// </summary>
public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordValidator"/> class.
    /// <summary>
    /// Initializes validation rules for ResetPasswordRequest.
    /// </summary>
    /// <remarks>
    /// Validation rules:
    /// - Email must be a valid email format.
    /// - Password must satisfy the application's password requirements.
    /// - ConfirmPassword must satisfy confirm-password rules and equal Password.
    /// - Code must be provided (not empty).
    /// </remarks>
    public ResetPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x => x.Password);
        RuleFor(request => request.Code).NotEmpty();
    }
}