// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
    }
}
