using Candy;
using Candy.Search;
using FluentAssertions;
using FakeItEasy;
using Xunit;

namespace CandyStoreTestProject.Search
{
    public class CandyShopTestWithFakeIt
    {
        public CandyShopTestWithFakeIt()
        {
            _localExpectedCandy.Brand = "Hershey";
            _localExpectedCandy.Type = "Bar";
            _nationalExpectedCandy.Brand = "Snickers";
            _nationalExpectedCandy.Brand = "Bar";
            _searchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
            _localSearchResult = new SearchResult(new SearchIndex(0, 1), new[] { _localExpectedCandy });
            _nationalSearchResult = new SearchResult(new SearchIndex(0, 1), new[] { _nationalExpectedCandy });
            _fakeLocalSearchEngine = A.Fake<ISearchEngine>();
            _fakeNationalSearchEngine = A.Fake<ISearchEngine>();
            _candyShop = new CandyShop(_fakeLocalSearchEngine, _fakeNationalSearchEngine);
            _emptySearchResult = new SearchResult(new SearchIndex(0,0), candy: new RawCandy[0]);
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchAndWillReturnTheTopItemInTheSearchResults()
        {
            A.CallTo(() => _fakeLocalSearchEngine.Search(A<SearchCriteria>.Ignored)).Returns(_localSearchResult);
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_localExpectedCandy);
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheLocalSearchWithKeywordTopSellerAndFirstTenResults()
        {
            A.CallTo(() => _fakeLocalSearchEngine.Search(_searchCriteria)).Returns(_localSearchResult);
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_localExpectedCandy);
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheNationalSearchWhenTheLocalSearchHasNoResults()
        {
            A.CallTo(() => _fakeLocalSearchEngine.Search(A<SearchCriteria>.Ignored)).Returns(_emptySearchResult);
            A.CallTo(() => _fakeNationalSearchEngine.Search(A<SearchCriteria>.Ignored)).Returns(_nationalSearchResult);
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_nationalExpectedCandy);
        }

        [Fact]
        public void BuyMostPopularCandyWillExecuteTheNationalSearchWithKeywordTopSellerAndFirstTenResults()
        {
            A.CallTo(() => _fakeLocalSearchEngine.Search(A<SearchCriteria>.Ignored)).Returns(_emptySearchResult);
            A.CallTo(() => _fakeNationalSearchEngine.Search(_searchCriteria)).Returns(_nationalSearchResult);
            var theCandy = _candyShop.BuyMostPopularCandy();
            theCandy.Should().Be(_nationalExpectedCandy);
        }

        private readonly RawCandy _localExpectedCandy;
        private readonly RawCandy _nationalExpectedCandy;
        private readonly SearchCriteria _searchCriteria;
        private readonly SearchResult _localSearchResult;
        private readonly SearchResult _nationalSearchResult;
        private readonly ISearchEngine _fakeLocalSearchEngine;
        private readonly ISearchEngine _fakeNationalSearchEngine;
        private readonly CandyShop _candyShop;
        private readonly SearchResult _emptySearchResult;
    }
}
