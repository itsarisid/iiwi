using iiwi.SearchEngine.Configuration;
using iiwi.SearchEngine.Facets;
using iiwi.SearchEngine.Helpers;
using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Queries;
using iiwi.SearchEngine.Results;
using Lucene.Net.Facet;
using Lucene.Net.Facet.Taxonomy;
using Lucene.Net.Facet.Taxonomy.Directory;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace iiwi.SearchEngine.Services;

/// <summary>
/// Reads and searches documents from a Lucene index with faceted search support
/// </summary>
/// <typeparam name="T">Document type implementing IDocument interface</typeparam>
/// <remarks>
/// Handles all search operations including field-specific, all-fields, and full-text searches.
/// Supports faceted filtering, pagination, and result transformation from Lucene documents to domain objects.
/// Uses lazy initialization for index readers and searchers.
/// </remarks>
internal sealed class DocumentReader<T> : IDocumentReader<T> where T : IDocument
{
    private readonly IIndexConfiguration<T> _configuration;
    private DirectoryReader? _indexDirectoryReader;
    private IndexSearcher? _searcher;

    /// <summary>
    /// Initializes a new document reader with the specified index configuration
    /// </summary>
    /// <param name="configuration">Index configuration providing index location and facet settings</param>
    public DocumentReader(IIndexConfiguration<T> configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Performs a field-specific search with optional facet filtering
    /// </summary>
    /// <param name="searchQuery">Query containing field-specific search terms and filters</param>
    /// <returns>Search results with pagination and facet information</returns>
    public SearchResult<T> Search(FieldSpecificSearchQuery searchQuery)
    {
        Init();

        var query = LuceneQueryBuilder.ConstructQuery<T>(searchQuery.SearchTerms, searchQuery.Type);
        query = AddFacetsQueries(searchQuery.Facets, query);

        return PerformSearch(query, searchQuery.PageNumber, searchQuery.PageSize);
    }

    /// <summary>
    /// Performs a search across all string fields with the same search term
    /// </summary>
    /// <param name="searchQuery">Query containing the search term and filters</param>
    /// <returns>Search results with pagination and facet information</returns>
    /// <remarks>
    /// Searches the same term across all string fields in the document
    /// </remarks>
    public SearchResult<T> Search(AllFieldsSearchQuery searchQuery)
    {
        Init();

        var searchDictionary = DocumentFieldsHelper.GetStringField<T>()
            .ToDictionary(fieldName => fieldName, _ => searchQuery.SearchTerm);

        var query = LuceneQueryBuilder.ConstructQuery<T>(searchDictionary, searchQuery.Type);
        query = AddFacetsQueries(searchQuery.Facets, query);

        return PerformSearch(query, searchQuery.PageNumber, searchQuery.PageSize);
    }

    /// <summary>
    /// Performs a full-text search with fuzzy and wildcard matching across all fields
    /// </summary>
    /// <param name="searchQuery">Query containing the full-text search term and filters</param>
    /// <returns>Search results with pagination and facet information</returns>
    /// <remarks>
    /// Uses fuzzy matching for typo tolerance and wildcard matching for prefix searches
    /// </remarks>
    public SearchResult<T> Search(FullTextSearchQuery searchQuery)
    {
        Init();

        Query query = new MatchAllDocsQuery();

        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTerm))
        {
            query = LuceneQueryBuilder.ConstructFulltextSearchQuery<T>(searchQuery);
        }

        query = AddFacetsQueries(searchQuery.Facets, query);

        return PerformSearch(query, searchQuery.PageNumber, searchQuery.PageSize);
    }

    /// <summary>
    /// Executes the search query and processes the results
    /// </summary>
    private SearchResult<T> PerformSearch(Query query, int pageNumber, int pageSize)
    {
        var searchTopDocs = _searcher!.Search(query, int.MaxValue);

        var items = GetItemsPaginated(pageNumber, pageSize, searchTopDocs);

        var result = new SearchResult<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = searchTopDocs.TotalHits
        };

        SetFacetResults(query, result);

        return result;
    }

    /// <summary>
    /// Retrieves paginated items from search results
    /// </summary>
    private IEnumerable<T> GetItemsPaginated(int pageNumber, int pageSize, TopDocs topDocs)
    {
        var documents = topDocs.ScoreDocs;
        var start = pageNumber * pageSize;
        var end = Math.Min(start + pageSize, documents.Length);

        IEnumerable<T> items;

        if (start > end)
        {
            items = Enumerable.Empty<T>();
        }
        else
        {
            items = documents[start..end].Select(hit => _searcher!.Doc(hit.Doc))
                .Select(d => d.ConvertToObjectOfType<T>());
        }

        return items;
    }

    /// <summary>
    /// Adds facet filtering to the base query using drill-down functionality
    /// </summary>
    private Query AddFacetsQueries(IDictionary<string, IEnumerable<string?>?>? facets, Query query)
    {
        if (_configuration.FacetConfiguration?.GetFacetConfig() == null)
        {
            return query;
        }

        if (facets == null || !facets.Any())
        {
            return query;
        }

        var drillDownQuery = new DrillDownQuery(_configuration.FacetConfiguration.GetFacetConfig(), query);

        foreach (var facet in facets)
        {
            if (facet.Value == null || !facet.Value.Any())
            {
                continue;
            }

            foreach (var value in facet.Value.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray())
            {
                drillDownQuery.Add(facet.Key, value);
            }
        }

        return drillDownQuery;
    }

    /// <summary>
    /// Initializes the index reader and searcher (lazy initialization)
    /// </summary>
    private void Init()
    {
        if (_indexDirectoryReader != null && _searcher != null)
        {
            return; // Already initialized
        }

        var indexPath = Path.Combine(Environment.CurrentDirectory, _configuration.IndexName);
        _indexDirectoryReader = DirectoryReader.Open(FSDirectory.Open(indexPath));
        _searcher = new IndexSearcher(_indexDirectoryReader);
    }

    /// <summary>
    /// Retrieves and sets facet results for the search query
    /// </summary>
    private void SetFacetResults(Query query, SearchResult<T> result)
    {
        if (_configuration.FacetConfiguration?.GetFacetConfig() == null)
        {
            return;
        }

        try
        {
            var facetsCollector = new FacetsCollector();
            FacetsCollector.Search(_searcher, query, 100, facetsCollector);

            using var facetsDirectory = FSDirectory.Open(_configuration.FacetConfiguration.IndexName);
            using var directoryTaxonomyReader = new DirectoryTaxonomyReader(facetsDirectory);

            var facetConfig = _configuration.FacetConfiguration.GetFacetConfig();
            var facets = new FastTaxonomyFacetCounts(directoryTaxonomyReader, facetConfig, facetsCollector);

            var facetResults = facets.GetAllDims(100).Select(facet => new FacetFilter
            {
                Name = facet.Dim,
                Values = facet.LabelValues.Select(p => new FacetValue
                {
                    Value = p.Label,
                    Count = (int)p.Value
                })
            });

            result.Facets = facetResults;
        }
        catch (Exception ex)
        {
            // Log facet retrieval errors in production
            // Consider adding logging here: _logger.LogError(ex, "Failed to retrieve facet results");
            result.Facets = Enumerable.Empty<FacetFilter>();
        }
    }

    /// <summary>
    /// Disposes resources and cleans up index readers
    /// </summary>
    public void Dispose()
    {
        _indexDirectoryReader?.Dispose();
        _searcher = null;
    }
}