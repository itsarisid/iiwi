
using FluentValidation;

namespace iiwi.Application.Account;

public class UpdateProfileValidator:AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(request => request.FirstName).Name();
        RuleFor(request => request.LastName).Name();
    }
}