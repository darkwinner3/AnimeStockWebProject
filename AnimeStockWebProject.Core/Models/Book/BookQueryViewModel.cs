namespace AnimeStockWebProject.Core.Models.Book
{
    using AnimeStockWebProject.Core.Models.BookType;
    using AnimeStockWebProject.Infrastructure.Data.Enums;
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
            BookTypes = new List<BookTypeViewModel>();
            SelectedBookTypeIds = new List<int>();
        }

        public BookSortEnum BookSortEnum { get; set; }

        public Pager Pager { get; set; } = null!;

        public int currentPage { get; set; }

        public PrintTypeEnum PrintType { get; set; }

        public IEnumerable<TagViewModel> BookTags { get; set; }

        public IEnumerable<BookTypeViewModel> BookTypes { get; set; }

        public IEnumerable<BookViewModel> BookViewModels { get; set; }

        public IEnumerable<int> SelectedBookTypeIds { get; set; }

        public IEnumerable<int> SelectedTagIds { get; set; }
    }
}
