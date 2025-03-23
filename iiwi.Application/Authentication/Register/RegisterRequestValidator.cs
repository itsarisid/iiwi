// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.Password).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x=>x.Password);
    }
}
