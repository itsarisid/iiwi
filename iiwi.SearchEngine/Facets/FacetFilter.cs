﻿
namespace iiwi.SearchEngine.Facets;

public class FacetFilter
{
    /// <summary>
    /// Facet name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Facet values
    /// </summary>
    public IEnumerable<FacetValue>? Values { get; set; }
}

