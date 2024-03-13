namespace AnimeStockWebProject.Core.Models.Pager
{
    using static Common.GeneralAplicaitonConstants;
    public class Pager
    {
        public Pager(int totalItems, int currentPage)
        {
            Configure(totalItems, currentPage);
        }

        private void Configure(int totalItems, int currentPage)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / DefaultPageSize);
            int startPage = Math.Max(1, currentPage - 2);
            int endPage = Math.Min(startPage + 3, totalPages);

            if(currentPage > endPage && endPage != 0)
            {
                currentPage = endPage;
                startPage = Math.Max(1, currentPage - 3);
                endPage = Math.Min(startPage + 3, totalPages);
            }

            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = DefaultPageSize;
            StartPage = startPage;
            EndPage = endPage;
            TotalItems = totalItems;
        }

        //Total number of entity records
        public int TotalItems {  get; private set; }

        //The active page on page bar
        public int CurrentPage { get; private set; }

        //The size of number of records displayed on the page
        public int PageSize { get; private set; }

        //Total number of pages on page bar
        public int TotalPages { get; private set; }

        //The page number which is first on page bar
        public int StartPage { get; private set; }

        //The page number which is last on page bar
        public int EndPage { get; private set; }
    }
}
