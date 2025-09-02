using iiwi.SearchEngine.Models;
using Lucene.Net.Facet;

namespace iiwi.SearchEngine.Configuration;

/// <summary>
/// Configures faceted search options for document types
/// </summary>
/// <typeparam name="T">The document type implementing IDocument</typeparam>
/// <remarks>
/// Provides configuration for faceted search functionality, allowing customization
/// of multi-valued fields and index-specific settings for enhanced search capabilities.
/// </remarks>
public class FacetConfiguration<T> where T : IDocument
{
    /// <summary>
    /// Fields that contain multiple values for faceted search
    /// </summary>
    /// <remarks>
    /// Fields specified here will be treated as multi-valued in facet calculations.
    /// This is useful for tags, categories, or other fields with multiple values.
    /// </remarks>
    public IEnumerable<string>? MultiValuedFields { get; set; }

    /// <summary>
    /// Name of the Lucene index for faceted search
    /// </summary>
    /// <remarks>
    /// This index name must match the corresponding search index configuration.
    /// </remarks>
    public required string IndexName { get; set; }

    /// <summary>
    /// Builds the Lucene FacetsConfig from current configuration
    /// </summary>
    /// <returns>Configured FacetsConfig instance</returns>
    /// <remarks>
    /// Internal method that converts the configuration into Lucene-specific
    /// facet settings for search operations.
    /// </remarks>
    internal FacetsConfig GetFacetConfig()
    {
        var facetsConfig = new FacetsConfig();

        if (MultiValuedFields == null)
        {
            return facetsConfig;
        }

        foreach (var field in MultiValuedFields)
        {
            facetsConfig.SetMultiValued(field, true);
        }

        return facetsConfig;
    }
}