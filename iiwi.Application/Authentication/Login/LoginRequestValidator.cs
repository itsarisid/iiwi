// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
    }
}
