
using FluentValidation;

namespace iiwi.Application.Account;

/// <summary>
/// Validator for UpdateProfileRequest.
/// </summary>
public class UpdateProfileValidator:AbstractValidator<UpdateProfileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProfileValidator"/> class.
    /// </summary>
    public UpdateProfileValidator()
    {
        RuleFor(request => request.FirstName).Name();
        RuleFor(request => request.LastName).Name();
    }
}