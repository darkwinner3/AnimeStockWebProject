using AnimeStockWebProject.Core.Models.Order;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IOrderService
    {
        Task OrderBookAsync(BookOrderDetailsViewModel bookOrderViewModel, Guid userId);
    }
}
