// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginRequestValidator"/> class.
    /// </summary>
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
    }
}
