using iiwi.SearchEngine.Configuration;
using iiwi.SearchEngine.Engine;
using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace iiwi.SearchEngine;

/// <summary>
/// Provides extension methods for registering search engine services with dependency injection
/// </summary>
/// <remarks>
/// Contains static methods to simplify the registration of search engine components
/// with Microsoft's dependency injection container. Handles proper lifecycle management
/// for search engine services.
/// </remarks>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers search engine services with the dependency injection container
    /// </summary>
    /// <typeparam name="T">The document type implementing IDocument interface</typeparam>
    /// <param name="serviceCollection">The service collection to add services to</param>
    /// <param name="configuration">Index configuration for search engine setup</param>
    /// <returns>The service collection for method chaining</returns>
    /// <remarks>
    /// Registers the following services with appropriate lifetimes:
    /// - IIndexConfiguration: Singleton (configuration is shared)
    /// - IDocumentWriter: Singleton (thread-safe index writing)
    /// - IDocumentReader: Scoped (per-request search operations)
    /// - ISearchEngine: Scoped (main search interface)
    ///
    /// Ensure the configuration is properly set up before calling this method.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddSearchEngineServices<Product>(new ProductIndexConfiguration());
    /// </code>
    /// </example>
    public static IServiceCollection AddSearchEngineServices<T>(this IServiceCollection serviceCollection,
        IIndexConfiguration<T> configuration) where T : IDocument
    {
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddSingleton<IDocumentWriter<T>, DocumentWriter<T>>();
        serviceCollection.AddScoped<IDocumentReader<T>, DocumentReader<T>>();
        serviceCollection.AddScoped<ISearchEngine<T>, SearchEngine<T>>();

        return serviceCollection;
    }
}