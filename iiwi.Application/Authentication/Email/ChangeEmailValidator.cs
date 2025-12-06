// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ChangeEmailRequest.
/// </summary>
public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeEmailValidator"/> class.
    /// </summary>
    public ChangeEmailValidator()
    {
        RuleFor(request => request.NewEmail).Email();
    }
}
