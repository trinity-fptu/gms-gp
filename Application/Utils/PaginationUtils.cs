namespace Application.Utils
{
    public class Pagination<T>
    {
        public int TotalItemsCount { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

        // Set the maximum amount of items in one page
        private const int MaxPageSize = 100;

        public int TotalPagesCount
        {
            get
            {
                var tmp = TotalItemsCount / PageSize;
                if (TotalItemsCount % PageSize == 0)
                {
                    return tmp;
                }
                return tmp + 1;
            }
        }

        private int _pageIndex = 0;

        // Auto re-assign pageIndex
        // if pageIndex is greater than or equal to TotalPagesCount
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value >= TotalPagesCount ? TotalPagesCount - 1 : value;
            }
        }

        public bool Next => PageIndex + 1 < TotalPagesCount;
        public bool Previous => PageIndex > 0;
        public ICollection<T> Items { get; set; }
    }
}
