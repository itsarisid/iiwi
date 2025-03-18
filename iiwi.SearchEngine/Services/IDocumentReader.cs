using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Queries;
using iiwi.SearchEngine.Results;

namespace iiwi.SearchEngine.Services;

internal interface IDocumentReader<T> where T : IDocument
{
    SearchResult<T> Search(FieldSpecificSearchQuery searchQuery);

    SearchResult<T> Search(AllFieldsSearchQuery searchQuery);

    SearchResult<T> Search(FullTextSearchQuery searchQuery);
}
