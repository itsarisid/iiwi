// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class LoginWithRecoveryCodeValidator : AbstractValidator<LoginWithRecoveryCodeRequest>
{
    public LoginWithRecoveryCodeValidator()
    {
        RuleFor(request => request.RecoveryCode).NotEmpty();
    }
}
