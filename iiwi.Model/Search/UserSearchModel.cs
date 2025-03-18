using iiwi.SearchEngine.Facets;
using iiwi.SearchEngine.Models;

namespace iiwi.Model.Search;

public class CustomerSearchModel : IDocument
{
    public string UniqueKey => TConst;

    public required string TConst { get; set; }

    [FacetProperty]
    public required string Name { get; set; }
}
