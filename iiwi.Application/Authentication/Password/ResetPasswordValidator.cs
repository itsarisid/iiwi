// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ResetPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
    }
}
