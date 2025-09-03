using iiwi.SearchEngine.Helpers;
using iiwi.SearchEngine.Models;
using Lucene.Net.Index;
using Lucene.Net.Search;

namespace iiwi.SearchEngine.Queries;

/// <summary>
/// Builds Lucene queries from various search criteria and types
/// </summary>
/// <remarks>
/// Provides static methods to construct different types of Lucene queries
/// based on field-specific search criteria or full-text search requirements.
/// Handles query type selection, field validation, and query combination.
/// </remarks>
internal static class LuceneQueryBuilder
{
    /// <summary>
    /// Constructs a Lucene query based on field-specific search criteria
    /// </summary>
    /// <typeparam name="T">Document type implementing IDocument</typeparam>
    /// <param name="searchFields">Dictionary of field names and their search values</param>
    /// <param name="searchType">Type of search to perform (exact, prefix, fuzzy)</param>
    /// <returns>Constructed Lucene query for execution</returns>
    /// <remarks>
    /// Returns MatchAllDocsQuery if no valid search criteria are provided.
    /// Only supports string and string[] field types for searching.
    /// Combines multiple field queries with SHOULD occurrence (OR logic).
    /// </remarks>
    internal static Query ConstructQuery<T>(IDictionary<string, string?>? searchFields, SearchType searchType)
        where T : IDocument
    {
        // Handle empty or null search criteria - return match-all query
        if (searchFields == null || searchFields.Count == 0 ||
            searchFields.All(p => string.IsNullOrWhiteSpace(p.Value)))
        {
            return new MatchAllDocsQuery();
        }

        var query = new BooleanQuery();
        var instance = Activator.CreateInstance<T>();

        foreach (var (fieldName, value) in searchFields)
        {
            // Skip empty search values
            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            // Validate field exists and is searchable type
            var type = instance.GetType().GetProperty(fieldName)?.PropertyType;
            if (type == null || (type != typeof(string) && type != typeof(string[])))
            {
                continue; // Skip unsupported field types
            }

            // Create appropriate query based on search type
            Query searchQuery = searchType switch
            {
                SearchType.ExactMatch => new TermQuery(new Term(fieldName, value)),
                SearchType.PrefixMatch => new PrefixQuery(new Term(fieldName, value)),
                SearchType.FuzzyMatch => new FuzzyQuery(new Term(fieldName, value)),
                _ => new TermQuery(new Term(fieldName, value)) // Default to exact match
            };

            // Add to boolean query with OR logic
            query.Add(searchQuery, Occur.SHOULD);
        }

        // If no valid queries were added, return match-all
        if (query.Clauses.Count == 0)
        {
            return new MatchAllDocsQuery();
        }

        return query;
    }

    /// <summary>
    /// Constructs a full-text search query across all string fields
    /// </summary>
    /// <typeparam name="T">Document type implementing IDocument</typeparam>
    /// <param name="searchQuery">Full-text search query parameters</param>
    /// <returns>Boolean query combining fuzzy and wildcard searches across all fields</returns>
    /// <remarks>
    /// Searches across all string fields in the document type using both
    /// fuzzy matching (for typo tolerance) and wildcard matching (for prefix searches).
    /// Uses OR logic between all field queries for comprehensive results.
    /// </remarks>
    internal static BooleanQuery ConstructFulltextSearchQuery<T>(FullTextSearchQuery searchQuery) where T : IDocument
    {
        // Get all searchable string fields from the document type
        var fields = DocumentFieldsHelper.GetStringField<T>();

        var query = new BooleanQuery();

        foreach (var field in fields)
        {
            // Create fuzzy query for typo-tolerant matching
            var fuzzyQuery = new FuzzyQuery(new Term(field, searchQuery.SearchTerm));

            // Create wildcard query for prefix matching
            var wildcardQuery = new WildcardQuery(new Term(field, $"{searchQuery.SearchTerm}*"));

            // Add both query types with OR logic
            query.Add(fuzzyQuery, Occur.SHOULD);
            query.Add(wildcardQuery, Occur.SHOULD);
        }

        return query;
    }
}