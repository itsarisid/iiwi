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
    /// </summary>
    public ConfirmEmailValidator()
    {
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}
