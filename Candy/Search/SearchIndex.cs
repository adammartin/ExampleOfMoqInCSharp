namespace Candy.Search
{
    public struct SearchIndex
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

        private readonly int _start;
        private readonly int _end;
    }
}