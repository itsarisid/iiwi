// Ignore Spelling: Validator

namespace Architecture.Application.Authentication
{
    public class ResetPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(request => request.Email).Email();
        }
    }
}
