// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for ForgotPasswordRequest.
/// </summary>
public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForgotPasswordValidator"/> class.
    /// <summary>
    /// Initializes a <see cref="ForgotPasswordValidator"/> that enforces a valid email format for the request's <c>Email</c> property.
    /// <summary>
    /// Initializes a validator that enforces a valid email format for the request's <c>Email</c> property.
    /// </summary>
    public ForgotPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
    }
}