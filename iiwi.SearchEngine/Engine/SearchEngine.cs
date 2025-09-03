using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Queries;
using iiwi.SearchEngine.Results;
using iiwi.SearchEngine.Services;
using Lucene.Net.Index;

namespace iiwi.SearchEngine.Engine;

/// <summary>
/// Core search engine implementation for document indexing and retrieval
/// </summary>
/// <typeparam name="T">Document type implementing IDocument interface</typeparam>
/// <remarks>
/// Provides comprehensive search functionality including CRUD operations,
/// multiple query types, and paginated results. Wraps Lucene.NET functionality
/// with a clean, type-safe API.
/// </remarks>
internal class SearchEngine<T> : ISearchEngine<T> where T : IDocument
{
    private readonly IDocumentReader<T> _documentReader;
    private readonly IDocumentWriter<T> _documentWriter;

    /// <summary>
    /// Initializes a new search engine instance
    /// </summary>
    /// <param name="documentReader">Reader service for search operations</param>
    /// <param name="documentWriter">Writer service for index modifications</param>
    /// <remarks>
    /// Automatically initializes the document writer upon creation
    /// </remarks>
    public SearchEngine(IDocumentReader<T> documentReader, IDocumentWriter<T> documentWriter)
    {
        _documentReader = documentReader;
        _documentWriter = documentWriter;
        _documentWriter.Init(); // Initialize index writer on creation
    }

    /// <summary>
    /// Adds a single document to the search index
    /// </summary>
    /// <param name="document">Document to add to the index</param>
    public void Add(T document)
    {
        _documentWriter.AddDocument(document);
    }

    /// <summary>
    /// Adds multiple documents to the search index
    /// </summary>
    /// <param name="documents">Collection of documents to add</param>
    /// <remarks>
    /// More efficient than individual Add calls for bulk operations
    /// </remarks>
    public void AddRange(IEnumerable<T> documents)
    {
        _documentWriter.AddDocuments(documents);
    }

    /// <summary>
    /// Releases all search engine resources
    /// </summary>
    /// <remarks>
    /// Important to call this when done to flush writes and release file handles
    /// </remarks>
    public void DisposeResources()
    {
        _documentWriter.Dispose();
    }

    /// <summary>
    /// Updates an existing document in the search index
    /// </summary>
    /// <param name="document">Document with updated values</param>
    /// <remarks>
    /// Effectively removes and re-adds the document with new values
    /// </remarks>
    public void Update(T document)
    {
        _documentWriter.UpdateDocument(document);
    }

    /// <summary>
    /// Removes a document from the search index
    /// </summary>
    /// <param name="document">Document to remove</param>
    public void Remove(T document)
    {
        _documentWriter.RemoveDocument(document);
    }

    /// <summary>
    /// Executes a search query and returns paginated results
    /// </summary>
    /// <param name="searchQuery">Query parameters and filters</param>
    /// <returns>Search results with pagination metadata</returns>
    /// <exception cref="ArgumentException">Thrown for unsupported query types</exception>
    /// <remarks>
    /// Handles empty index gracefully by returning empty results instead of throwing
    /// </remarks>
    public SearchResult<T> Search(SearchQuery searchQuery)
    {
        try
        {
            // Pattern matching to route to appropriate search method
            return searchQuery switch
            {
                FieldSpecificSearchQuery fieldSpecificSearchQuery => Search(fieldSpecificSearchQuery),
                AllFieldsSearchQuery allFieldsSearchQuery => Search(allFieldsSearchQuery),
                FullTextSearchQuery fullTextSearchQuery => Search(fullTextSearchQuery),
                _ => throw new ArgumentException($"Invalid search query type: {searchQuery.GetType().Name}")
            };
        }
        catch (IndexNotFoundException)
        {
            // Gracefully handle empty index - return empty results instead of error
            return new SearchResult<T>
            {
                Items = Enumerable.Empty<T>(),
                PageNumber = searchQuery.PageNumber,
                PageSize = searchQuery.PageSize,
                TotalItems = 0
            };
        }
    }

    /// <summary>
    /// Removes all documents from the search index
    /// </summary>
    /// <remarks>
    /// Completely clears the index. Use with caution.
    /// </remarks>
    public void RemoveAll()
    {
        _documentWriter.Clear();
    }

    // Private search methods that delegate to the appropriate reader method

    /// <summary>
    /// Executes a field-specific search query
    /// </summary>
    private SearchResult<T> Search(FieldSpecificSearchQuery searchQuery)
    {
        return _documentReader.Search(searchQuery);
    }

    /// <summary>
    /// Executes an all-fields search query
    /// </summary>
    private SearchResult<T> Search(AllFieldsSearchQuery searchQuery)
    {
        return _documentReader.Search(searchQuery);
    }

    /// <summary>
    /// Executes a full-text search query
    /// </summary>
    private SearchResult<T> Search(FullTextSearchQuery searchQuery)
    {
        return _documentReader.Search(searchQuery);
    }
}