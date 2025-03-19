
namespace iiwi.Model.Records;

public sealed record UserModel
{
    public long Id { get; init; }

    public required string Name { get; init; }

    public required string Email { get; init; }
    public bool Status { get; set; }
    public required string PhoneNumber { get; set; }
}
