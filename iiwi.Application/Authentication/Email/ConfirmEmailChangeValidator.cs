// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class ConfirmEmailChangeValidator : AbstractValidator<ConfirmEmailChangeRequest>
{
    public ConfirmEmailChangeValidator()
    {
        RuleFor(request => request.Email).Email();
        RuleFor(request => request.UserId).NotEmpty();
        RuleFor(request => request.Code).NotEmpty();
    }
}
