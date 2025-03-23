// Ignore Spelling: Validator
namespace Architecture.Application.Authentication
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(request => request.UserId).NotEmpty();
            RuleFor(request => request.Code).NotEmpty();
        }
    }

}
