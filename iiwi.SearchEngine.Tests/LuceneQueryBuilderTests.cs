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
        /// <summary>
        /// Gets or sets the UniqueKey.
        /// </summary>
        public string UniqueKey { get; init; } = string.Empty;
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; init; } = string.Empty;
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; init; } = string.Empty;
        /// <summary>
        /// Gets or sets the Tags.
        /// </summary>
        public string[] Tags { get; init; } = Array.Empty<string>();
        /// <summary>
        /// Gets or sets the Count.
        /// </summary>
        public int Count { get; init; }
    }

    /// <summary>
    /// Executes the ConstructQuery_ReturnsMatchAll_WhenSearchFieldsAreNull operation.
    /// </summary>
    [Fact]
    public void ConstructQuery_ReturnsMatchAll_WhenSearchFieldsAreNull()
    {
        var query = LuceneQueryBuilder.ConstructQuery<SampleDocument>(null, SearchType.ExactMatch);

        Assert.IsType<MatchAllDocsQuery>(query);
    }

    /// <summary>
    /// Executes the ConstructQuery_ReturnsMatchAll_WhenOnlyUnsupportedFieldsProvided operation.
    /// </summary>
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

    /// <summary>
    /// Executes the ConstructQuery_BuildsExpectedQueryType operation.
    /// </summary>
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

    /// <summary>
    /// Executes the ConstructFulltextSearchQuery_CreatesFuzzyAndWildcardQueriesForEachField operation.
    /// </summary>
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
