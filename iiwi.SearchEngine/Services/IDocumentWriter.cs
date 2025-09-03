using iiwi.SearchEngine.Models;
using System.Diagnostics.CodeAnalysis;

namespace iiwi.SearchEngine.Services;

/// <summary>
/// Defines the contract for writing documents to a search index
/// </summary>
/// <typeparam name="T">The document type implementing IDocument interface</typeparam>
/// <remarks>
/// Provides a generic interface for performing CRUD operations against a document index.
/// Implementations should handle index initialization, document transformation, and
/// proper resource management. Supports both single and bulk operations.
/// </remarks>
internal interface IDocumentWriter<in T> where T : IDocument
{
    /// <summary>
    /// Initializes the index writer and related resources
    /// </summary>
    /// <remarks>
    /// Should be called before any write operations. Implementations may use
    /// lazy initialization, but this method ensures resources are ready.
    /// </remarks>
    void Init();

    /// <summary>
    /// Adds a single document to the search index
    /// </summary>
    /// <param name="generic">The document to add to the index</param>
    /// <exception cref="ArgumentNullException">Thrown when document is null</exception>
    /// <remarks>
    /// The document should be converted to appropriate index format and stored.
    /// Commits changes to make the document immediately searchable.
    /// </remarks>
    void AddDocument([NotNull] T generic);

    /// <summary>
    /// Adds multiple documents to the search index in a batch operation
    /// </summary>
    /// <param name="documents">Collection of documents to add</param>
    /// <remarks>
    /// More efficient than individual AddDocument calls for bulk operations.
    /// Commits changes after all documents are processed.
    /// </remarks>
    void AddDocuments(IEnumerable<T> documents);

    /// <summary>
    /// Updates an existing document in the search index
    /// </summary>
    /// <param name="generic">The document with updated values</param>
    /// <exception cref="ArgumentNullException">Thrown when document is null</exception>
    /// <remarks>
    /// Uses the document's UniqueKey to locate and replace the existing document.
    /// Effectively performs a delete followed by an add operation.
    /// </remarks>
    void UpdateDocument([NotNull] T generic);

    /// <summary>
    /// Removes a document from the search index
    /// </summary>
    /// <param name="generic">The document to remove</param>
    /// <exception cref="ArgumentNullException">Thrown when document is null</exception>
    /// <remarks>
    /// Uses the document's UniqueKey to identify the document to remove.
    /// The document will no longer appear in search results.
    /// </remarks>
    void RemoveDocument([NotNull] T generic);

    /// <summary>
    /// Removes all documents from the search index
    /// </summary>
    /// <remarks>
    /// Completely clears the index. This operation cannot be undone.
    /// Use with caution as it removes all indexed data.
    /// </remarks>
    void Clear();

    /// <summary>
    /// Releases all resources used by the document writer
    /// </summary>
    /// <remarks>
    /// Implementations should flush any pending writes and release file handles.
    /// This method should be called when the writer is no longer needed.
    /// </remarks>
    void Dispose();
}