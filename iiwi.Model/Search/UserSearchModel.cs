using iiwi.SearchEngine.Facets;
using iiwi.SearchEngine.Models;

namespace iiwi.Model.Search;

public class CustomerSearchModel : IDocument
{
    /// <summary>
    /// Gets the UniqueKey.
    /// </summary>
    public string UniqueKey => TConst;

    /// <summary>
    /// Gets or sets the TConst.
    /// </summary>
    public required string TConst { get; set; }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [FacetProperty]
    public required string Name { get; set; }
}
