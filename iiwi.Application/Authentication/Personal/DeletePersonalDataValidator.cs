// Ignore Spelling: Validator

using FluentValidation;

namespace iiwi.Application.Authentication;

public class DeletePersonalDataValidator : AbstractValidator<DeletePersonalDataRequest>
{
    public DeletePersonalDataValidator()
    {
        RuleFor(request => request.Password).Password();
    }
}
