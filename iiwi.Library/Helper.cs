using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iiwi.Library;

public static class Helper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FromUrlAttribute : Attribute { }

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
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (destination == null)
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
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (destination == null)
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

    // Usage in request classes
    public class AddRoleClaimRequest
    {
        [FromUrl]
        public int RoleId { get; init; }  // This comes from URL

        public string ClaimType { get; init; } = string.Empty;  // This comes from body
        public string ClaimValue { get; init; } = string.Empty; // This comes from body
    }
}
