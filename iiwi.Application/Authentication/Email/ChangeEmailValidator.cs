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
    /// <summary>
    /// Initializes a validator that requires the ChangeEmailRequest.NewEmail property to be a well-formed email address.
    /// </summary>
    public ChangeEmailValidator()
    {
        RuleFor(request => request.NewEmail).Email();
    }
}