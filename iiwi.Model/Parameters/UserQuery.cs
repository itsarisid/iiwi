namespace iiwi.Model.Parameters;

/// <summary>
/// Query for users.
/// </summary>
public class UserQuery : QueryParameters
{
    /// <summary>
    /// Gets or sets the name type facets.
    /// </summary>
    public string[] NameTypeFacets { get; set; }
}