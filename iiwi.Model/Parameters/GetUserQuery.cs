namespace iiwi.Model.Parameters;

/// <summary>
/// Query for getting a user.
/// </summary>
internal class GetUserQuery : UserQuery
{
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
}