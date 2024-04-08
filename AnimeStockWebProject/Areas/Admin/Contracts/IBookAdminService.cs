using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IBookAdminService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync(Pager pager);
        Task<int> GetAllBooksCountAsync();

        Task<int> BookAddAsync(BookAddViewModel bookAddViewModel);

        Task CreateBookPicturesAsync(int bookId, BookAddViewModel bookAddViewModel);

        Task CreateCoverImgAsync(int bookId, BookAddViewModel bookAddViewModel);

        Task CreateBookFileAsync(int bookId, BookAddViewModel bookAddViewModel);

        Task<BookEditViewModel> GetBookToEditAsync(int bookId);

        Task EditBookByIdAsync(int bookId, BookEditViewModel bookEditViewModel);
        Task DeleteBookByIdAsync(int bookId);

        Task RecoverBookByIdAsync(int bookId);

        Task DeleteBooksJobAsync();
    }
}
