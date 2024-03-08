namespace AnimeStockWebProject.Core.Models.Book
{
    using BookTags;
    using Enum;
    using Pager;
    public class BookQueryViewModel
    {
        public BookQueryViewModel()
        {
            BookViewModels = new List<BookViewModel>();
            BookTags = new List<TagViewModel>();
            SelectedTagIds = new List<int>();
        }

        public BookSortEnum BookSortEnum { get; set; }

        public Pager Pager { get; set; } = null!;

        public int currentPage { get; set; }

        public IEnumerable<TagViewModel> BookTags { get; set; }

        public IEnumerable<BookViewModel> BookViewModels { get; set; }

        public IEnumerable<int> SelectedTagIds { get; set; }
    }
}
