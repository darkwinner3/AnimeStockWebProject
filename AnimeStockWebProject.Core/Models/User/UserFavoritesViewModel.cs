namespace AnimeStockWebProject.Core.Models.User
{
    using AnimeStockWebProject.Core.Models.Book;
    using Pager;
    public class UserFavoritesViewModel
    {
        public UserFavoritesViewModel()
        {
            BookViewModels = new List<BookViewModel>();
        }

        public Pager Pager { get; set; } = null!;

        public int currentPage { get; set; }


        public IEnumerable<BookViewModel> BookViewModels { get; set; }
    }
}
