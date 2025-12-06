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
    /// </summary>
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().EmailAddress();

    /// <summary>
    /// Validates that the file collection is not empty.
    /// </summary>
    public static IRuleBuilderOptions<T, IEnumerable<BinaryFile>> Files<T>(this IRuleBuilder<T, IEnumerable<BinaryFile>> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the grid parameters are not empty.
    /// </summary>
    public static IRuleBuilderOptions<T, GridParameters> Grid<T>(this IRuleBuilder<T, GridParameters> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the GUID is not empty.
    /// </summary>
    public static IRuleBuilderOptions<T, Guid> Guid<T>(this IRuleBuilder<T, Guid> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the ID is greater than 0.
    /// </summary>
    public static IRuleBuilderOptions<T, long> Id<T>(this IRuleBuilder<T, long> builder) => builder.NotEmpty().GreaterThan(0);

    /// <summary>
    /// Validates that the login string is not empty.
    /// </summary>
    public static IRuleBuilderOptions<T, string> Login<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty();

    /// <summary>
    /// Validates that the name is not empty and has a minimum length of 3.
    /// </summary>
    public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(3);

    /// <summary>
    /// Validates that the password is not empty and has a minimum length of 8.
    /// </summary>
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(8);

    /// <summary>
    /// Validates that the confirm password is not empty and has a minimum length of 8.
    /// </summary>
    public static IRuleBuilderOptions<T, string> ConfirmPassword<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(8);
}

