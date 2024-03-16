using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IBookService
    {
        Task<int> GetCountAsync(BookQueryViewModel bookQueryViewModel);

        Task<AllBooksSortedDataModel> GetAllBooksSortedDataModelAsync(Guid? userId, BookQueryViewModel bookQueryViewModel);

        Task<BookInfoViewModel> GetBookByIdAsync(int bookId, Pager pager);

        Task<int> GetBookCommentsCountAsync(int bookId);

        Task<bool> BookExistsAsync(int bookId);

        Task<IEnumerable<BookNameViewModel>> GetBookByTitleAsync(string title, int bookId);
    }
}
