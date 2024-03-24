using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IBookService
    {
        Task<int> GetCountAsync(BookQueryViewModel bookQueryViewModel);

        Task<AllBooksSortedDataModel> GetAllBooksSortedDataModelAsync(Guid? userId, BookQueryViewModel bookQueryViewModel);

        Task<BookInfoViewModel> GetBookByIdAsync(int bookId, Pager pager, Guid? userId);

        Task<int> GetBookCommentsCountAsync(int bookId);

        Task<bool> BookExistsAsync(int bookId);

        Task<IEnumerable<BookNameViewModel>> GetBookByTitleAsync(string title, int bookId);

        Task AddItemToFavorites(int bookId, Guid userId);
        Task RemoveItemFromFavorites(int bookId, Guid userId);

        Task<byte[]> GetBookFileAsync(string filePath, int pageCount);

        Task<BookOrderViewModel> GetBookToOrder(BookInfoViewModel bookInfoViewModel);
    }
}
