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
    /// </summary>
    public ConfirmEmailChangeValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}
