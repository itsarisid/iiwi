using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Queries;
using iiwi.SearchEngine.Results;

namespace iiwi.SearchEngine.Services;

/// <summary>
/// Defines the contract for reading and searching documents from a search index
/// </summary>
/// <typeparam name="T">The document type implementing IDocument interface</typeparam>
/// <remarks>
/// Provides a generic interface for performing various types of searches against
/// a document index. Implementations should handle query execution, result pagination,
/// and facet processing where applicable.
/// </remarks>
internal interface IDocumentReader<T> where T : IDocument
{
    /// <summary>
    /// Performs a field-specific search with targeted query terms
    /// </summary>
    /// <param name="searchQuery">Query containing field-specific search terms and filters</param>
    /// <returns>Search results with pagination metadata and facet information</returns>
    /// <remarks>
    /// Allows searching specific fields with different values and search types.
    /// Useful for advanced search forms where users can target specific document properties.
    /// </remarks>
    SearchResult<T> Search(FieldSpecificSearchQuery searchQuery);

    /// <summary>
    /// Performs a search across all searchable fields with the same term
    /// </summary>
    /// <param name="searchQuery">Query containing the search term to apply to all fields</param>
    /// <returns>Search results with pagination metadata and facet information</returns>
    /// <remarks>
    /// Searches the same term across all string fields in the document type.
    /// Useful for simple search interfaces where users want comprehensive coverage.
    /// </remarks>
    SearchResult<T> Search(AllFieldsSearchQuery searchQuery);

    /// <summary>
    /// Performs a full-text search with fuzzy and wildcard matching
    /// </summary>
    /// <param name="searchQuery">Query containing the full-text search term</param>
    /// <returns>Search results with pagination metadata and facet information</returns>
    /// <remarks>
    /// Uses fuzzy matching for typo tolerance and wildcard matching for prefix searches.
    /// Provides the most user-friendly search experience with tolerance for minor errors.
    /// </remarks>
    SearchResult<T> Search(FullTextSearchQuery searchQuery);
}