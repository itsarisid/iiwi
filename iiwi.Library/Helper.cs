using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace iiwi.Library;

/// <summary>
/// Helper class for various operations.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Attribute to mark properties that should be populated from URL parameters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FromUrlAttribute : Attribute { }

    /// <summary>
    /// Combines URL parameters and request body into a single request object.
    /// </summary>
    /// <typeparam name="TUrlParams">The type of URL parameters.</typeparam>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <param name="urlParams">The URL parameters.</param>
    /// <param name="body">The request body.</param>
    /// <returns>The combined request object.</returns>
    public static TRequest CombineParameters<TUrlParams, TRequest>(TUrlParams urlParams, TRequest body)
        where TUrlParams : class, new()
        where TRequest : class, new()
    {
        var combined = Activator.CreateInstance<TRequest>();
        var urlType = typeof(TUrlParams);
        var requestType = typeof(TRequest);

        // Copy all properties from body
        foreach (var prop in requestType.GetProperties().Where(p => p.CanWrite))
        {
            prop.SetValue(combined, prop.GetValue(body));
        }

        // Process URL parameters
        foreach (var urlProp in urlType.GetProperties())
        {
            var requestProp = requestType.GetProperties()
                .FirstOrDefault(p => p.Name.Equals(urlProp.Name, StringComparison.OrdinalIgnoreCase));

            if (requestProp?.CanWrite == true)
            {
                var urlValue = urlProp.GetValue(urlParams);
                if (urlValue != null)
                {
                    // Override if marked with [FromUrl] attribute OR if the property is currently null/default
                    var hasFromUrlAttribute = requestProp.GetCustomAttribute<FromUrlAttribute>() != null;
                    var currentValue = requestProp.GetValue(combined);

                    if (hasFromUrlAttribute || currentValue == null || IsDefaultValue(currentValue))
                    {
                        requestProp.SetValue(combined, urlValue);
                    }
                }
            }
        }

        return combined;
    }

    /// <summary>
    /// Merges URL parameters and request body into a single request object.
    /// </summary>
    /// <typeparam name="TUrlParams">The type of URL parameters.</typeparam>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <param name="urlParams">The URL parameters.</param>
    /// <param name="body">The request body.</param>
    /// <returns>The merged request object.</returns>
    public static TRequest MergeParameters<TUrlParams, TRequest>(TUrlParams urlParams, TRequest body)
    where TUrlParams : class, new()
    where TRequest : class, new()
    {
        var properties = CopyPropertiesWithMissing(urlParams, body);
        var combined = Activator.CreateInstance<TRequest>();
        var requestType = typeof(TRequest);

        foreach (var prop in properties)
        {
            var propertyInfo = requestType.GetProperty(prop.Key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(combined, prop.Value);
            }
        }
        return combined;
    }


    /// <summary>
    /// Copies all matching property values from source to destination
    /// </summary>
    public static void CopyProperties<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source.Equals(default(TSource)))
            throw new ArgumentNullException(nameof(source));

        if (EqualityComparer<TDestination>.Default.Equals(destination, default(TDestination)))
            throw new ArgumentNullException(nameof(destination));

        PropertyInfo[] sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        PropertyInfo[] destProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sourceProp in sourceProperties)
        {
            if (!sourceProp.CanRead)
                continue;

            var destProp = Array.Find(destProperties, p =>
                p.Name == sourceProp.Name &&
                p.PropertyType == sourceProp.PropertyType &&
                p.CanWrite);

            if (destProp != null)
            {
                var value = sourceProp.GetValue(source);
                destProp.SetValue(destination, value);
            }
        }
    }

    /// <summary>
    /// Copies properties from source to a dynamic ExpandoObject that includes all properties
    /// </summary>
    public static dynamic CopyPropertiesWithMissing<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (EqualityComparer<TSource>.Default.Equals(source, default(TSource)))
            throw new ArgumentNullException(nameof(source));

        if (destination.Equals(default(TDestination)))
            throw new ArgumentNullException(nameof(destination));

        var result = new Dictionary<string, object>();

        // Add all destination properties first
        PropertyInfo[] destProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var destProp in destProperties)
        {
            if (destProp.CanRead)
            {
                result[destProp.Name] = destProp.GetValue(destination);
            }
        }

        // Copy/override with source properties
        PropertyInfo[] sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var sourceProp in sourceProperties)
        {
            if (sourceProp.CanRead)
            {
                result[sourceProp.Name] = sourceProp.GetValue(source);
            }
        }

        return result;
    }

    private static bool IsDefaultValue(object value)
    {
        if (value == null) return true;
        var type = value.GetType();
        if (type.IsValueType)
        {
            return value.Equals(Activator.CreateInstance(type));
        }
        return false;
    }



}
