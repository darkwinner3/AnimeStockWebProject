using AnimeStockWebProject.Areas.Admin.Models.BookType;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IBookTypeService
    {
        Task<IEnumerable<BookTypeViewModel>> GetAllBookTypesAsync();
    }
}
