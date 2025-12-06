namespace iiwi.Model.Parameters;
/// <summary>
/// Base class for query parameters.
/// </summary>
public abstract class QueryParameters
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; } = 0;

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
