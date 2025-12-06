
namespace iiwi.Model.Records;

/// <summary>
/// Represents a user model.
/// </summary>
public sealed record UserModel
{
    /// <summary>
    /// Gets the ID.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is active.
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public required string PhoneNumber { get; set; }
}
