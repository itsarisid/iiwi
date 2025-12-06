using iiwi.SearchEngine.Facets;
using iiwi.SearchEngine.Models;

namespace iiwi.Model.Search;

/// <summary>
/// Model for searching customers.
/// </summary>
public class CustomerSearchModel : IDocument
{
    /// <inheritdoc />
    public string UniqueKey => TConst;

    /// <summary>
    /// Gets or sets the TConst.
    /// </summary>
    public required string TConst { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [FacetProperty]
    public required string Name { get; set; }
}
