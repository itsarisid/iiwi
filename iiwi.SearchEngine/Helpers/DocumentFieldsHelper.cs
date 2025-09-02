using iiwi.SearchEngine.Models;

namespace iiwi.SearchEngine.Helpers;

/// <summary>
/// Provides helper methods for working with document field metadata
/// </summary>
/// <remarks>
/// Utility class for reflection-based field analysis of document types.
/// Helps identify searchable fields and manage field metadata dynamically.
/// </remarks>
internal static class DocumentFieldsHelper
{
    /// <summary>
    /// Retrieves all string field names from a document type
    /// </summary>
    /// <typeparam name="T">Document type implementing IDocument interface</typeparam>
    /// <returns>Collection of string field names available for searching</returns>
    /// <remarks>
    /// Uses reflection to analyze the document type and identify all string properties.
    /// Excludes the UniqueKey property as it's typically used for identification rather than search.
    /// Useful for building dynamic search interfaces or field validation.
    /// </remarks>
    internal static IEnumerable<string> GetStringField<T>() where T : IDocument
    {
        var instance = Activator.CreateInstance<T>();

        // Get all properties of the document type
        return typeof(T).GetProperties()
            // Select property names and their types
            .Select(property => new
            {
                FieldName = property.Name,
                PropertyType = property.PropertyType
            })
            // Exclude the UniqueKey property (typically used for identification, not search)
            .Where(p => p.FieldName != nameof(IDocument.UniqueKey))
            // Filter for string properties only
            .Where(p => p.PropertyType == typeof(string))
            // Return just the field names
            .Select(p => p.FieldName);
    }
}