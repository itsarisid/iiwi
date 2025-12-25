using DotNetCore.Objects;
using FluentValidation;

namespace iiwi.Application;

/// <summary>
/// Provides extension methods for FluentValidation rules.
/// </summary>
public static class Validators
{
    /// <summary>
    /// Validates that the property is a valid email address.
    /// <summary>
/// Adds rules that require the string to be non-empty and to match an email address format.
/// </summary>
/// <typeparam name="T">The type that contains the string property being validated.</typeparam>
/// <param name="builder">The rule builder for the string property.</param>
/// <returns>Rule builder options for continuing validation of the string property.</returns>
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().EmailAddress();

    /// <summary>
    /// Validates that the file collection is not empty.
    /// <summary>
/// Validates that a collection of BinaryFile contains at least one item.
/// </summary>
/// <typeparam name="T">The parent model type being validated.</typeparam>
/// <param name="builder">The rule builder for the collection property.</param>
/// <returns>A rule builder configured to require the collection to contain at least one BinaryFile.</returns>
    public static IRuleBuilderOptions<T, IEnumerable<BinaryFile>> Files<T>(this IRuleBuilder<T, IEnumerable<BinaryFile>> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the grid parameters are not empty.
    /// <summary>
/// Adds a validation rule requiring the GridParameters value to be non-empty.
/// </summary>
/// <returns>Rule builder options configured with a not-empty rule for GridParameters.</returns>
    public static IRuleBuilderOptions<T, GridParameters> Grid<T>(this IRuleBuilder<T, GridParameters> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the GUID is not empty.
    /// <summary>
/// Validates that a Guid property is not Guid.Empty.
/// </summary>
/// <returns>The rule builder options with the NotEmpty rule applied.</returns>
    public static IRuleBuilderOptions<T, Guid> Guid<T>(this IRuleBuilder<T, Guid> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the ID is greater than 0.
    /// <summary>
/// Adds validation rules that require a long identifier to be present and greater than zero.
/// </summary>
/// <param name="builder">The rule builder for a long identifier.</param>
/// <returns>The rule builder options with NotEmpty and GreaterThan(0) applied.</returns>
    public static IRuleBuilderOptions<T, long> Id<T>(this IRuleBuilder<T, long> builder) => builder.NotEmpty().GreaterThan(0);

    /// <summary>
    /// Validates that the login string is not empty.
    /// <summary>
/// Adds a validation rule that requires the login string to be non-empty.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <returns>The rule builder configured to require a non-empty string.</returns>
    public static IRuleBuilderOptions<T, string> Login<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the name is not empty and has a minimum length of 3.
    /// <summary>
/// Ensures a name value is not empty and has a minimum length of 3 characters.
/// </summary>
/// <returns>The rule builder options configured to require a non-empty value with at least 3 characters.</returns>
    public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(3);

    /// <summary>
    /// Validates that the password is not empty and has a minimum length of 8.
    /// <summary>
/// Adds validation rules that require the string to be non-empty and at least 8 characters long.
/// </summary>
/// <typeparam name="T">The type that contains the property being validated.</typeparam>
/// <param name="builder">The rule builder for the string property to validate.</param>
/// <returns>The rule builder options with the password rules applied.</returns>
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(8);

    /// <summary>
    /// Validates that the confirm password is not empty and has a minimum length of 8.
    /// <summary>
/// Adds validation rules for a password confirmation string.
/// </summary>
/// <typeparam name="T">The type of the object being validated.</typeparam>
/// <returns>The rule builder configured to require a non-empty string with at least 8 characters.</returns>
    public static IRuleBuilderOptions<T, string> ConfirmPassword<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(8);
}
