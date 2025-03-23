// Ignore Spelling: Validator

namespace Architecture.Application.Authentication
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.Email).Email();
            RuleFor(request => request.Password).Password();
        }
    }
}
