using System;

namespace Candy.Search
{
    public class SearchCriteria : IEquatable<SearchCriteria>
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SearchCriteria) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_searchIndex.GetHashCode() * 397) ^ (_searchTerm != null ? _searchTerm.GetHashCode() : 0);
            }
        }

        public static bool operator ==(SearchCriteria left, SearchCriteria right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SearchCriteria left, SearchCriteria right)
        {
            return !Equals(left, right);
        }

        public bool Equals(SearchCriteria other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _searchIndex.Equals(other._searchIndex) && string.Equals(_searchTerm, other._searchTerm);
        }

        private readonly SearchIndex _searchIndex;
        private readonly String _searchTerm;
    }
}

