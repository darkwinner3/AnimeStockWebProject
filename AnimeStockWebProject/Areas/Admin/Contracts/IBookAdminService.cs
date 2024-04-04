using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IBookAdminService
    {
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync(Pager pager);
        Task<int> GetAllBooksCountAsync();
    }
}
