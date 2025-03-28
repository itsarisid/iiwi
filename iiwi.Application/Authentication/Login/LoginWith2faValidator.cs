// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

internal class LoginWith2faValidator : AbstractValidator<LoginWith2faRequest>
{
    public LoginWith2faValidator()
    {
        RuleFor(request => request.TwoFactorCode).NotEmpty();
        RuleFor(request => request.RememberMachine).NotNull();
    }
}
