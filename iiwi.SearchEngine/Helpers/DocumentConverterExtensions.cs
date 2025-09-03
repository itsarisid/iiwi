using iiwi.SearchEngine.Facets;
using iiwi.SearchEngine.Models;
using Lucene.Net.Documents;
using Lucene.Net.Facet;
using System.Reflection;

namespace iiwi.SearchEngine.Helpers;

/// <summary>
/// Provides conversion methods between domain objects and Lucene documents
/// </summary>
/// <remarks>
/// Handles bidirectional conversion between custom objects and Lucene Document instances.
/// Supports primitive types, strings, and string arrays with facet configuration.
/// Note: Nested objects and complex types beyond arrays are not supported.
/// </remarks>
internal static class DocumentConverterExtensions
{
    /// <summary>
    /// Converts a Lucene Document back to a domain object instance
    /// </summary>
    /// <typeparam name="T">The type of object to create, must implement IDocument</typeparam>
    /// <param name="document">The Lucene Document to convert</param>
    /// <returns>A new instance of type T populated with document data</returns>
    /// <remarks>
    /// Handles primitive type conversion and string arrays. Does not support nested objects.
    /// Returns default values for missing fields rather than throwing exceptions.
    /// </remarks>
    internal static T ConvertToObjectOfType<T>(this Document document) where T : IDocument
    {
        var instance = Activator.CreateInstance<T>();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.PropertyType.IsArray)
            {
                SetArrayField(document, property, instance);
            }
            else
            {
                SetField(document, property, instance);
            }
        }

        return instance;
    }

    /// <summary>
    /// Sets a single-value field from document to property
    /// </summary>
    private static void SetField<T>(Document document, PropertyInfo property, T instance)
        where T : IDocument
    {
        var field = document.GetField(property.Name);

        if (field == null)
        {
            return; // Field not found in document, skip
        }

        var fieldValue = field.GetStringValue();
        SetPropertyValue(property, instance, fieldValue);
    }

    /// <summary>
    /// Sets an array field from document to property
    /// </summary>
    private static void SetArrayField<T>(Document document, PropertyInfo property, T instance)
        where T : IDocument
    {
        var fields = document.GetFields(property.Name);

        if (fields == null)
        {
            return; // No array fields found, skip
        }

        var fieldValues = fields.Select(field => field.GetStringValue()).ToArray();
        SetPropertyValues(property, instance, fieldValues);
    }

    /// <summary>
    /// Converts a domain object to a Lucene Document for indexing
    /// </summary>
    /// <typeparam name="T">The type of object to convert, must implement IDocument</typeparam>
    /// <param name="instance">The object instance to convert</param>
    /// <returns>A Lucene Document ready for indexing</returns>
    /// <remarks>
    /// Handles facet field creation for properties marked with [FacetProperty] attribute.
    /// Stores all values as text fields. Arrays are stored as multiple fields with same name.
    /// </remarks>
    internal static Document ConvertToDocument<T>(this T instance) where T : IDocument
    {
        var document = new Document();

        foreach (var property in typeof(T).GetProperties())
        {
            var value = property.GetValue(instance) ?? string.Empty;
            var fieldName = property.Name;
            var facetAttribute = Attribute.GetCustomAttribute(property, typeof(FacetProperty));

            if (!property.PropertyType.IsArray)
            {
                // Handle single-value properties
                var field = new TextField(fieldName, value.ToString(), Field.Store.YES);
                document.Add(field);

                if (facetAttribute != null)
                {
                    // Add facet field for faceted search
                    document.Add(new FacetField(property.Name, value.ToString()));
                }
            }
            else
            {
                // Handle array properties - store each element as separate field
                var array = (Array)value;
                foreach (var arrayItem in array)
                {
                    var fieldValue = arrayItem?.ToString() ?? string.Empty;

                    document.Add(new TextField(fieldName, fieldValue, Field.Store.YES));

                    if (facetAttribute != null)
                    {
                        document.Add(new FacetField(fieldName, fieldValue));
                    }
                }
            }
        }

        return document;
    }

    /// <summary>
    /// Sets property value with type conversion from string
    /// </summary>
    private static void SetPropertyValue<T>(PropertyInfo property, T instance, string fieldValue)
        where T : IDocument
    {
        var propertyType = property.PropertyType;

        try
        {
            // Handle all supported primitive types with appropriate parsing
            if (propertyType == typeof(string) && property.Name != nameof(IDocument.UniqueKey))
            {
                property.SetValue(instance, fieldValue);
            }
            else if (propertyType == typeof(int))
            {
                property.SetValue(instance, int.Parse(fieldValue));
            }
            else if (propertyType == typeof(long))
            {
                property.SetValue(instance, long.Parse(fieldValue));
            }
            else if (propertyType == typeof(double))
            {
                property.SetValue(instance, double.Parse(fieldValue));
            }
            else if (propertyType == typeof(bool))
            {
                property.SetValue(instance, bool.Parse(fieldValue));
            }
            else if (propertyType == typeof(byte))
            {
                property.SetValue(instance, byte.Parse(fieldValue));
            }
            else if (propertyType == typeof(sbyte))
            {
                property.SetValue(instance, sbyte.Parse(fieldValue));
            }
            else if (propertyType == typeof(char))
            {
                property.SetValue(instance, char.Parse(fieldValue));
            }
            else if (propertyType == typeof(decimal))
            {
                property.SetValue(instance, decimal.Parse(fieldValue));
            }
            else if (propertyType == typeof(float))
            {
                property.SetValue(instance, float.Parse(fieldValue));
            }
            else if (propertyType == typeof(uint))
            {
                property.SetValue(instance, uint.Parse(fieldValue));
            }
            else if (propertyType == typeof(nint))
            {
                property.SetValue(instance, nint.Parse(fieldValue));
            }
            else if (propertyType == typeof(nuint))
            {
                property.SetValue(instance, nuint.Parse(fieldValue));
            }
            else if (propertyType == typeof(ulong))
            {
                property.SetValue(instance, ulong.Parse(fieldValue));
            }
            else if (propertyType == typeof(short))
            {
                property.SetValue(instance, short.Parse(fieldValue));
            }
            else if (propertyType == typeof(ushort))
            {
                property.SetValue(instance, ushort.Parse(fieldValue));
            }
        }
        catch (Exception ex) when (ex is FormatException || ex is OverflowException)
        {
            // Gracefully handle parsing errors - leave property at default value
            // Consider logging this in production code
        }
    }

    /// <summary>
    /// Sets array property values from string array
    /// </summary>
    private static void SetPropertyValues<T>(PropertyInfo property, T instance, string[] fieldValues)
    {
        var propertyType = property.PropertyType;

        // Currently only supports string arrays
        if (propertyType == typeof(string[]))
        {
            property.SetValue(instance, fieldValues);
        }
        // Additional array types could be supported here:
        // else if (propertyType == typeof(int[])) { ... }
    }
}