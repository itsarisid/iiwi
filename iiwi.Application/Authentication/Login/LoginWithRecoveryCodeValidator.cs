// Ignore Spelling: Validator

namespace Architecture.Application.Authentication
{
    public class LoginWithRecoveryCodeValidator : AbstractValidator<LoginWithRecoveryCodeRequest>
    {
        public LoginWithRecoveryCodeValidator()
        {
            RuleFor(request => request.RecoveryCode).NotEmpty();
        }
    }
}
