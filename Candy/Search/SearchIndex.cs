using System;

namespace Candy.Search
{
    public struct SearchIndex : IEquatable<SearchIndex>
    {
         public SearchIndex(int start, int end) : this()
        {
            _start = start;
            _end = end;
        }

        public int Start()
        {
            return _start;
        }

        public int End()
        {
            return _end;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SearchIndex && Equals((SearchIndex)obj);
        }

        public bool Equals(SearchIndex other)
        {
            return _start == other._start && _end == other._end;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_start*397) ^ _end;
            }
        }

        public static bool operator ==(SearchIndex left, SearchIndex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SearchIndex left, SearchIndex right)
        {
            return !left.Equals(right);
        }

        private readonly int _start;
        private readonly int _end;
    }
}