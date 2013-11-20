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
            _searchResult = new SearchResult(new SearchIndex(0, 1), new[]{_localExpectedCandy});
            _mockLocalSearchEngine = new Mock<ISearchEngine>();
            _mockNationalSearchEngine = new Mock<ISearchEngine>();
            _candyShop = new CandyShop(_mockLocalSearchEngine.Object, _mockNationalSearchEngine.Object);
        }

        [Fact]
        public void BuyMostPopularCandyWillSearchLocalSearchEngineForMostPopularResults()
        {
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.IsAny<SearchCriteria>())).Returns(_searchResult).Verifiable();
            _candyShop.BuyMostPopularCandy();
            _mockLocalSearchEngine.Verify();
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchWithKeywordTopSellerAndFirstTenResults()
        {
            var searchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(searchCriteria)))).Returns(_searchResult).Verifiable();
            _candyShop.BuyMostPopularCandy();
            _mockLocalSearchEngine.Verify();
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchAndWillReturnTheTopItemInTheSearchResults()
        {
            var searchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
            _mockLocalSearchEngine.Setup(aSearch => aSearch.Search(It.Is<SearchCriteria>(criteria => criteria.Equals(searchCriteria)))).Returns(_searchResult).Verifiable();
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_localExpectedCandy);
        }

        private readonly SearchResult _searchResult;
        private readonly RawCandy _localExpectedCandy;
        private readonly Mock<ISearchEngine> _mockLocalSearchEngine;
        private readonly Mock<ISearchEngine> _mockNationalSearchEngine;
        private readonly CandyShop _candyShop;
    }
}
