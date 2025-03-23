// Ignore Spelling: Validator


namespace Architecture.Application.Authentication
{
    public class DeletePersonalDataValidator : AbstractValidator<DeletePersonalDataRequest>
    {
        public DeletePersonalDataValidator()
        {
            RuleFor(request => request.Password).Password();
        }
    }
}
