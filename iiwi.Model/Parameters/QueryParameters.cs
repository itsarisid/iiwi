namespace iiwi.Model.Parameters;
public abstract class QueryParameters
{
    /// <summary>
    /// Gets or sets the PageNumber.
    /// </summary>
    public int PageNumber { get; set; } = 0;

    /// <summary>
    /// Gets or sets the PageSize.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
