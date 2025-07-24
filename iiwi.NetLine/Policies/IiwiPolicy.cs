using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace iiwi.NetLine.Policies
{
    /// <summary>
    /// Custom output caching policy that extends standard behavior to:
    /// 1. Cache POST requests (in addition to GET/HEAD)
    /// 2. Cache 301 Moved Permanently responses
    /// 3. Apply security restrictions for authenticated requests
    /// </summary>
    /// <remarks>
    /// This policy implements safety checks to prevent caching of:
    /// - Authenticated requests
    /// - Responses with cookies
    /// - Non-successful status codes (except 301)
    /// </remarks>
    public sealed class IiwiPolicy : IOutputCachePolicy
    {
        /// <summary>
        /// Singleton instance of the caching policy
        /// </summary>
        public static readonly IiwiPolicy Instance = new();

        /// <summary>
        /// Determines if the current request should be cached and sets up caching rules
        /// </summary>
        ValueTask IOutputCachePolicy.CacheRequestAsync(
            OutputCacheContext context,
            CancellationToken cancellationToken)
        {
            // Check if this request meets our caching criteria
            var attemptOutputCaching = AttemptOutputCaching(context);

            // Configure caching behavior
            context.EnableOutputCaching = true;          // Enable caching pipeline
            context.AllowCacheLookup = attemptOutputCaching;  // Allow checking cache
            context.AllowCacheStorage = attemptOutputCaching; // Allow storing responses
            context.AllowLocking = true;                // Enable cache locking

            // Vary cache entries by all query string parameters
            context.CacheVaryByRules.QueryKeys = "*";

            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Called when serving a response from cache (no implementation needed)
        /// </summary>
        ValueTask IOutputCachePolicy.ServeFromCacheAsync(
            OutputCacheContext context,
            CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Validates whether the response should be stored in cache
        /// </summary>
        ValueTask IOutputCachePolicy.ServeResponseAsync(
            OutputCacheContext context,
            CancellationToken cancellationToken)
        {
            var response = context.HttpContext.Response;

            // Never cache responses that set cookies
            if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            // Only cache successful responses (200 OK) and permanent redirects (301)
            if (response.StatusCode != StatusCodes.Status200OK &&
                response.StatusCode != StatusCodes.Status301MovedPermanently)
            {
                context.AllowCacheStorage = false;
                return ValueTask.CompletedTask;
            }

            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Determines if the current request is eligible for caching
        /// </summary>
        private static bool AttemptOutputCaching(OutputCacheContext context)
        {
            var request = context.HttpContext.Request;

            // Only cache GET, HEAD, and POST requests
            if (!HttpMethods.IsGet(request.Method) &&
                !HttpMethods.IsHead(request.Method) &&
                !HttpMethods.IsPost(request.Method))
            {
                return false;
            }

            // Never cache requests with:
            // - Authorization headers
            // - Authenticated users
            if (!StringValues.IsNullOrEmpty(request.Headers.Authorization) ||
                request.HttpContext.User?.Identity?.IsAuthenticated == true)
            {
                return false;
            }

            return true;
        }
    }
}