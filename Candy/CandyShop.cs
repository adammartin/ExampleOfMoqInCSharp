using System;
using Candy.Search;

namespace Candy
{
    public struct RawCandy
    {
        public string Brand;
        public string Type;
    }

    public interface ICandyShop
    {
        RawCandy BuyMostPopularCandy();
    }

    public class CandyShop : ICandyShop
    {
        public CandyShop(ISearchEngine localSearchEngine, ISearchEngine nationalSearchEngine)
        {
            _localSearchEngine = localSearchEngine;
            _nationalSearchEngine = nationalSearchEngine;
        }

        public RawCandy BuyMostPopularCandy()
        {
            var searchResult = _localSearchEngine.Search(SearchCriteria);
            if (searchResult.GetSearchIndex().Start() == searchResult.GetSearchIndex().End())
            {
                return _nationalSearchEngine.Search(SearchCriteria).GetCandyResults()[0];
            }
            return searchResult.GetCandyResults()[0];
        }

        private readonly ISearchEngine _localSearchEngine;
        private readonly ISearchEngine _nationalSearchEngine;
        private static readonly SearchCriteria SearchCriteria = new SearchCriteria(new SearchIndex(0, 10), "TopSeller");
    }
}
