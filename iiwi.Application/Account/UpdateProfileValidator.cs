using FluentValidation;

namespace iiwi.Application.Account;

/// <summary>
/// Validator for UpdateProfileRequest.
/// </summary>
public class UpdateProfileValidator:AbstractValidator<UpdateProfileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProfileValidator"/> class.
    /// <summary>
    /// Initializes a new <see cref="UpdateProfileValidator"/> and configures validation rules for update requests by applying the Name() rule to the FirstName and LastName properties.
    /// <summary>
    /// Initializes a new instance of <see cref="UpdateProfileValidator"/> and configures its validation rules.
    /// </summary>
    /// <remarks>
    /// Applies the `Name()` validation rule to the `FirstName` and `LastName` properties of <c>UpdateProfileRequest</c>.
    /// </remarks>
    public UpdateProfileValidator()
    {
        RuleFor(request => request.FirstName).Name();
        RuleFor(request => request.LastName).Name();
    }
}