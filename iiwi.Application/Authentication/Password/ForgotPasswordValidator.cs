// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForgotPasswordValidator"/> class.
    /// </summary>
    public ForgotPasswordValidator()
    {
        RuleFor(request => request.Email).Email();
    }
}
