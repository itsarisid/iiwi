// Ignore Spelling: Validator
using FluentValidation;

namespace iiwi.Application.Authentication;

/// <summary>
/// Validator for LoginWith2faRequest.
/// </summary>
internal class LoginWith2faValidator : AbstractValidator<LoginWith2faRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginWith2faValidator"/> class.
    /// </summary>
    public LoginWith2faValidator()
    {
        RuleFor(request => request.TwoFactorCode).NotEmpty();
        RuleFor(request => request.RememberMachine).NotNull();
    }
}
