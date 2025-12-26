// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ConfirmEmailChangeRequest.
/// </summary>
public class ConfirmEmailChangeValidator : AbstractValidator<ConfirmEmailChangeRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailChangeValidator"/> class.
    /// <summary>
    /// Initializes a ConfirmEmailChangeValidator and configures validation rules for ConfirmEmailChangeRequest.
    /// </summary>
    /// <remarks>
    /// Validation rules:
    /// - Email must be a valid email address.
    /// - UserId must not be empty.
    /// - Code must not be empty.
    /// </remarks>
    public ConfirmEmailChangeValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}