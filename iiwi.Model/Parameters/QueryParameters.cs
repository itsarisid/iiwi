namespace iiwi.Model.Parameters;
public abstract class QueryParameters
{
    public int PageNumber { get; set; } = 0;

    public int PageSize { get; set; } = 10;
}
