using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Search
{
    struct SearchResult
    {
        public SearchResult(SearchIndex searchIndex, RawCandy[] candy)
        {
            _searchIndex = searchIndex;
            _candy = candy;
        }

        public RawCandy[] GetCandyResults()
        {
            return _candy.Clone() as RawCandy[];
        }

        public SearchIndex GetSearchIndex()
        {
            return _searchIndex;
        }

        private readonly RawCandy[] _candy;
        private readonly SearchIndex _searchIndex;
    }
}
