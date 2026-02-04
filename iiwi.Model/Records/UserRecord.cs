
namespace iiwi.Model.Records;

public sealed record UserModel
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public required string Email { get; init; }
    /// <summary>
    /// Gets or sets the Status.
    /// </summary>
    public bool Status { get; set; }
    /// <summary>
    /// Gets or sets the PhoneNumber.
    /// </summary>
    public required string PhoneNumber { get; set; }
}
