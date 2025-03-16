namespace iiwi.Model.Search;

public class CustomerSearchModel : IDocument
{
    public string UniqueKey => TConst;

    public required string TConst { get; set; }

    [FacetProperty]
    public required string Name { get; set; }
}
