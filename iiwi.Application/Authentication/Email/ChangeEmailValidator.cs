// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailValidator()
    {
        RuleFor(request => request.NewEmail).Email();
    }
}
