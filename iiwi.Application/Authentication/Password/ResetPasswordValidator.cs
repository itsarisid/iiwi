// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ResetPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordValidator"/> class.
    /// </summary>
    public ResetPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
    }
}
