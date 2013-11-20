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
            return _localSearchEngine.Search(new SearchCriteria(new SearchIndex(0, 10), "TopSeller")).GetCandyResults()[0];
        }

        private readonly ISearchEngine _localSearchEngine;
        private readonly ISearchEngine _nationalSearchEngine;
    }
}
