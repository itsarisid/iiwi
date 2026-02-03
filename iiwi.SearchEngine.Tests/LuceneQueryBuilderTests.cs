using System;
using System.Collections.Generic;
using iiwi.SearchEngine.Models;
using iiwi.SearchEngine.Queries;
using Lucene.Net.Search;
using Xunit;

namespace iiwi.SearchEngine.Tests;

public class LuceneQueryBuilderTests
{
    private sealed class SampleDocument : IDocument
    {
        public string UniqueKey { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string[] Tags { get; init; } = Array.Empty<string>();
        public int Count { get; init; }
    }

    [Fact]
    public void ConstructQuery_ReturnsMatchAll_WhenSearchFieldsAreNull()
    {
        var query = LuceneQueryBuilder.ConstructQuery<SampleDocument>(null, SearchType.ExactMatch);

        Assert.IsType<MatchAllDocsQuery>(query);
    }

    [Fact]
    public void ConstructQuery_ReturnsMatchAll_WhenOnlyUnsupportedFieldsProvided()
    {
        var searchFields = new Dictionary<string, string?>
        {
            ["Count"] = "5"
        };

        var query = LuceneQueryBuilder.ConstructQuery<SampleDocument>(searchFields, SearchType.ExactMatch);

        Assert.IsType<MatchAllDocsQuery>(query);
    }

    [Theory]
    [InlineData(SearchType.ExactMatch, typeof(TermQuery))]
    [InlineData(SearchType.PrefixMatch, typeof(PrefixQuery))]
    [InlineData(SearchType.FuzzyMatch, typeof(FuzzyQuery))]
    public void ConstructQuery_BuildsExpectedQueryType(SearchType searchType, Type expectedQueryType)
    {
        var searchFields = new Dictionary<string, string?>
        {
            ["Title"] = "search"
        };

        var query = LuceneQueryBuilder.ConstructQuery<SampleDocument>(searchFields, searchType);

        var booleanQuery = Assert.IsType<BooleanQuery>(query);
        var clause = Assert.Single(booleanQuery.Clauses);
        Assert.IsType(expectedQueryType, clause.Query);
    }

    [Fact]
    public void ConstructFulltextSearchQuery_CreatesFuzzyAndWildcardQueriesForEachField()
    {
        var searchQuery = new FullTextSearchQuery
        {
            SearchTerm = "focus"
        };

        var query = LuceneQueryBuilder.ConstructFulltextSearchQuery<SampleDocument>(searchQuery);

        Assert.Equal(4, query.Clauses.Count);
        Assert.Contains(query.Clauses, clause => clause.Query is FuzzyQuery);
        Assert.Contains(query.Clauses, clause => clause.Query is WildcardQuery);
    }
}
