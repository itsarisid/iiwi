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

        // Only override properties marked with [FromUrl] attribute
        foreach (var urlProp in urlType.GetProperties())
        {
            var requestProp = requestType.GetProperties()
                .FirstOrDefault(p => p.Name.Equals(urlProp.Name, StringComparison.OrdinalIgnoreCase) &&
                                    p.GetCustomAttribute<FromUrlAttribute>() != null);

            if (requestProp?.CanWrite == true)
            {
                var urlValue = urlProp.GetValue(urlParams);
                if (urlValue != null)
                {
                    requestProp.SetValue(combined, urlValue);
                }
            }
        }

        return combined;
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
