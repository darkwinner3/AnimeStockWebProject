using AnimeStockWebProject.Core.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IOrderService
    {
        Task OrderBookAsync(BookOrderDetailsViewModel bookOrderViewModel, Guid userId);

        Task<OrderInfoViewModel> GetBookOrderInfoAsync(Guid Id);

        Task<FileStreamResult> DownloadBook(string filePath);

        Task UpdateOrderStatusAsync();
    }
}
