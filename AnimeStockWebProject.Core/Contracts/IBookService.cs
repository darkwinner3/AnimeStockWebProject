using AnimeStockWebProject.Core.Models.Book;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IBookService
    {
        Task<int> GetCountAsync(BookQueryViewModel bookQueryViewModel);

        Task<AllBooksSortedDataModel> GetAllBooksSortedDataModelAsync(Guid? userId, BookQueryViewModel bookQueryViewModel);
    }
}
