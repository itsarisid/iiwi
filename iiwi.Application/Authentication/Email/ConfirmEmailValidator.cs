// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ConfirmEmailRequest.
/// </summary>
public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailValidator"/> class.
    /// <summary>
    /// Initializes a new instance of <see cref="ConfirmEmailValidator"/> and configures validation rules for confirming an email.
    /// </summary>
    /// <remarks>
    /// Ensures the request's <see cref="ConfirmEmailRequest.UserId"/> and <see cref="ConfirmEmailRequest.Code"/> are not empty.
    /// <summary>
    /// Configures validation rules for confirming a user's email.
    /// </summary>
    /// <remarks>
    /// Requires the request's <c>UserId</c> and <c>Code</c> properties to be present (not empty).
    /// </remarks>
    public ConfirmEmailValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}