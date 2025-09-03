using iiwi.SearchEngine.Configuration;
using iiwi.SearchEngine.Helpers;
using iiwi.SearchEngine.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Facet;
using Lucene.Net.Facet.Taxonomy.Directory;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Diagnostics.CodeAnalysis;
using LuceneDirectory = Lucene.Net.Store.Directory;

namespace iiwi.SearchEngine.Services;

/// <summary>
/// Writes documents to a Lucene index with support for faceted search
/// </summary>
/// <typeparam name="T">Document type implementing IDocument interface</typeparam>
/// <remarks>
/// Handles all index write operations including add, update, delete, and bulk operations.
/// Supports faceted indexing when configured. Implements proper resource disposal.
/// Uses lazy initialization for index writers.
/// </remarks>
internal sealed class DocumentWriter<T> : IDisposable, IDocumentWriter<T> where T : IDocument
{
    private readonly FacetsConfig? _facetsConfig;
    private readonly string _indexName;
    private readonly string? _facetIndexName;

    private LuceneDirectory? _facetIndexDirectory;
    private bool _initialized;
    private DirectoryTaxonomyWriter? _taxonomyWriter;
    private IndexWriter? _writer;

    /// <summary>
    /// Initializes a new document writer with the specified index configuration
    /// </summary>
    /// <param name="configuration">Index configuration providing index location and facet settings</param>
    /// <exception cref="ArgumentException">Thrown when index name is not configured</exception>
    public DocumentWriter(IIndexConfiguration<T> configuration)
    {
        if (string.IsNullOrWhiteSpace(configuration.IndexName))
        {
            throw new ArgumentException("Index name must be set before using DocumentWriter.");
        }

        _indexName = configuration.IndexName;
        _facetIndexName = configuration.FacetConfiguration?.IndexName;
        _facetsConfig = configuration.FacetConfiguration?.GetFacetConfig();
    }

    /// <summary>
    /// Initializes the index writer and taxonomy writer (lazy initialization)
    /// </summary>
    /// <remarks>
    /// Creates or opens the index directory and configures the analyzer.
    /// Initializes facet taxonomy writer if faceted search is configured.
    /// </remarks>
    public void Init()
    {
        if (_initialized)
        {
            return; // Already initialized
        }

        // Open the index directories
        var indexPath = Path.Combine(Environment.CurrentDirectory, _indexName);
        var indexDirectory = FSDirectory.Open(indexPath);

        // Create an analyzer to process the text
        const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;
        Analyzer standardAnalyzer = new StandardAnalyzer(luceneVersion);
        var indexConfig = new IndexWriterConfig(luceneVersion, standardAnalyzer)
        {
            OpenMode = OpenMode.CREATE_OR_APPEND, // Open existing index or create new
        };

        // Create the index writer with the above configuration
        _writer = new IndexWriter(indexDirectory, indexConfig);

        // Initialize facet taxonomy if configured
        if (_facetsConfig == null || string.IsNullOrWhiteSpace(_facetIndexName))
        {
            _initialized = true;
            return;
        }

        _facetIndexDirectory = FSDirectory.Open(_facetIndexName);
        _taxonomyWriter = new DirectoryTaxonomyWriter(_facetIndexDirectory);

        _initialized = true;
    }

    /// <summary>
    /// Adds a single document to the index
    /// </summary>
    /// <param name="generic">The document to add</param>
    /// <exception cref="ArgumentNullException">Thrown when document is null</exception>
    public void AddDocument([NotNull] T generic)
    {
        var document = generic.ConvertToDocument();
        _writer?.AddDocument(GetDocument(document));

        Commit();
    }

    /// <summary>
    /// Removes all documents from the index
    /// </summary>
    /// <remarks>
    /// Completely clears the index. This operation cannot be undone.
    /// </remarks>
    public void Clear()
    {
        _writer?.DeleteAll();
        Commit();
    }

    /// <summary>
    /// Adds multiple documents to the index in a batch operation
    /// </summary>
    /// <param name="documents">Collection of documents to add</param>
    /// <remarks>
    /// More efficient than individual AddDocument calls for bulk operations.
    /// Commits only once after all documents are added.
    /// </remarks>
    public void AddDocuments(IEnumerable<T> documents)
    {
        foreach (var generic in documents)
        {
            _writer?.AddDocument(GetDocument(generic));
        }

        Commit();
    }

    /// <summary>
    /// Updates an existing document in the index
    /// </summary>
    /// <param name="generic">The document with updated values</param>
    /// <remarks>
    /// Uses the document's UniqueKey to find and replace the existing document.
    /// Effectively performs a delete followed by an add operation.
    /// </remarks>
    public void UpdateDocument([NotNull] T generic)
    {
        var document = generic.ConvertToDocument();
        _writer?.UpdateDocument(new Term(nameof(IDocument.UniqueKey), generic.UniqueKey), GetDocument(document));

        Commit();
    }

    /// <summary>
    /// Removes a document from the index
    /// </summary>
    /// <param name="generic">The document to remove</param>
    /// <remarks>
    /// Uses the document's UniqueKey to identify the document to remove.
    /// </remarks>
    public void RemoveDocument([NotNull] T generic)
    {
        _writer?.DeleteDocuments(new Term(nameof(IDocument.UniqueKey), generic.UniqueKey));
        Commit();
    }

    /// <summary>
    /// Disposes all index resources and writers
    /// </summary>
    /// <remarks>
    /// Important to call this method to ensure all writes are flushed and resources are released.
    /// </remarks>
    public void Dispose()
    {
        _taxonomyWriter?.Dispose();
        _facetIndexDirectory?.Dispose();
        _writer?.Dispose();

        _initialized = false;
    }

    /// <summary>
    /// Commits all pending changes to the index
    /// </summary>
    /// <remarks>
    /// Flushes all changes to disk and makes them available for searching.
    /// Called automatically after each write operation.
    /// </remarks>
    private void Commit()
    {
        _writer?.Commit();
        _taxonomyWriter?.Commit();
    }

    /// <summary>
    /// Converts a domain object to a Lucene Document and applies facet configuration
    /// </summary>
    private Document GetDocument(T generic)
    {
        var document = generic.ConvertToDocument();
        return GetDocument(document);
    }

    /// <summary>
    /// Applies facet configuration to a Lucene Document if facets are enabled
    /// </summary>
    /// <param name="document">The Lucene Document to process</param>
    /// <returns>The document with facet configuration applied</returns>
    /// <remarks>
    /// If faceted search is configured, builds the document with facet taxonomy.
    /// Otherwise, returns the document unchanged.
    /// </remarks>
    private Document GetDocument(Document document)
    {
        return _facetsConfig != null ? _facetsConfig.Build(_taxonomyWriter, document) : document;
    }
}