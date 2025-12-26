// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for RegisterRequest.
/// </summary>
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterRequestValidator"/> class.
    /// <summary>
    /// Configures validation rules for <see cref="RegisterRequest"/>: verifies that Email is a valid email address, applies password validation to Password, and enforces that ConfirmPassword meets confirmation rules and equals Password.
    /// <summary>
    /// Initializes validation rules for RegisterRequest.
    /// </summary>
    /// <remarks>Validates that Email is a valid email address, Password meets password rules, and ConfirmPassword matches Password.</remarks>
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x=>x.Password);
    }
}