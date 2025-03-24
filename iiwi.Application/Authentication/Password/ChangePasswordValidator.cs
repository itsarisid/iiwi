// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(request => request.OldPassword).Password();
        RuleFor(request => request.NewPassword).Password();
        RuleFor(request => request.ConfirmPassword).ConfirmPassword().Equal(x=>x.NewPassword);
    }
}
