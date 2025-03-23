// Ignore Spelling: Validator

namespace Architecture.Application.Authentication
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(request => request.Email).Email();
        }
    }
}
