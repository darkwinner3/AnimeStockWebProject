using AnimeStockWebProject.Core.Models.Pager;

namespace AnimeStockWebProject.Areas.Admin.Models.Book.DataModels
{
    public class BookDataModel
    {
        public BookDataModel()
        {
            BookViewModels = new List<BookViewModel>();
        }

        public Pager Pager { get; set; } = null!;

        public IEnumerable<BookViewModel> BookViewModels { get; set; }
    }
}
