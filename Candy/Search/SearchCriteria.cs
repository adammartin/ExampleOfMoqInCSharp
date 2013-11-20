using System;

namespace Candy.Search
{
    public class SearchCriteria
    {
        public SearchCriteria(SearchIndex searchIndex, string searchTerm)
        {
            _searchIndex = searchIndex;
            _searchTerm = searchTerm;
        }

        public SearchIndex GetSearchIndex()
        {
            return _searchIndex;
        }

        public String GetSearchTerm()
        {
            return _searchTerm;
        }

        private readonly SearchIndex _searchIndex;
        private readonly String _searchTerm;
    }
}

