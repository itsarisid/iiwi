namespace iiwi.Model.Parameters;

/// <summary>
/// Query for searching users.
/// </summary>
internal class SearchUserQuery : UserQuery
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
}