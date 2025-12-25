// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for LoginRequest.
/// </summary>
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginRequestValidator"/> class.
    /// <summary>
    /// Initializes a new instance of <see cref="LoginRequestValidator"/> and configures validation rules for LoginRequest.
    /// </summary>
    /// <remarks>
    /// Enforces that Email has a valid email format and Password meets the configured password requirements.
    /// </remarks>
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
    }
}