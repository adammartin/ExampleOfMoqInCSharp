using Candy;
using Candy.Search;
using FluentAssertions;
using Moq;
using Xunit;

namespace CandyStoreTestProject.Search
{
    public class CandyShopTestWithMoq
    {
        public CandyShopTestWithMoq()
        {
            _localExpectedCandy.Brand = "Hershey";
            _localExpectedCandy.Type = "Bar";
            _nationalExpectedCandy.Brand = "Snickers";
            _nationalExpectedCandy.Brand = "Bar";
            _searchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
            _localSearchResult = new SearchResult(new SearchIndex(0, 1), new[] { _localExpectedCandy });
            _nationalSearchResult = new SearchResult(new SearchIndex(0, 1), new[] { _nationalExpectedCandy });
            _mockLocalSearchEngine = new Mock<ISearchEngine>();
            _mockNationalSearchEngine = new Mock<ISearchEngine>();
            _candyShop = new CandyShop(_mockLocalSearchEngine.Object, _mockNationalSearchEngine.Object);
            _emptySearchResult = new SearchResult(new SearchIndex(0,0), candy: new RawCandy[0]);
        }

        [Fact]
        public void BuyMostPopularCandyWillSearchLocalSearchEngineForMostPopularResults()
        {
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.IsAny<SearchCriteria>())).Returns(_localSearchResult).Verifiable();
            _candyShop.BuyMostPopularCandy();
            _mockLocalSearchEngine.Verify();
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchWithKeywordTopSellerAndFirstTenResults()
        {
            var searchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(searchCriteria)))).Returns(_localSearchResult).Verifiable();
            _candyShop.BuyMostPopularCandy();
            _mockLocalSearchEngine.Verify();
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchAndWillReturnTheTopItemInTheSearchResults()
        {
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(_searchCriteria)))).Returns(_localSearchResult);
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_localExpectedCandy);
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheNationalSearchWhenTheLocalSearchHasNoResults()
        {
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.IsAny<SearchCriteria>())).Returns(_emptySearchResult).Verifiable();
            _mockNationalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(_searchCriteria)))).Returns(_localSearchResult).Verifiable();
            _candyShop.BuyMostPopularCandy();
            _mockLocalSearchEngine.Verify();
        }

        [Fact]
        public void BuyMostPopularCandyWillReturnTheNationalSearchResultWhenTheNationalSearchResultIsCalled()
        {
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.IsAny<SearchCriteria>())).Returns(_emptySearchResult).Verifiable();
            _mockNationalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(_searchCriteria)))).Returns(_nationalSearchResult);
            var candyResults = _candyShop.BuyMostPopularCandy();
            candyResults.Should().Be(_nationalExpectedCandy);
            _mockLocalSearchEngine.Verify();
        }

        private readonly RawCandy _localExpectedCandy;
        private readonly RawCandy _nationalExpectedCandy;
        private readonly SearchCriteria _searchCriteria;
        private readonly SearchResult _localSearchResult;
        private readonly SearchResult _nationalSearchResult;
        private readonly Mock<ISearchEngine> _mockLocalSearchEngine;
        private readonly Mock<ISearchEngine> _mockNationalSearchEngine;
        private readonly CandyShop _candyShop;
        private readonly SearchResult _emptySearchResult;
    }
}
