using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static AnimeStockWebProject.Infrastructure.Data.Enums.StatusEnum;

namespace AnimeStockWebProject.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public OrderService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }

        public async Task OrderBookAsync(BookOrderDetailsViewModel bookOrderViewModel, Guid userId)
        {
            //giving random expected delivery date between 1 and 2 months 
            TimeSpan minSpan = TimeSpan.FromDays(30);
            TimeSpan maxSpan = TimeSpan.FromDays(60);

            Random rand = new Random();
            int totalDays = rand.Next((int)minSpan.TotalDays, (int)maxSpan.TotalDays);
            TimeSpan randomSpan = TimeSpan.FromDays(totalDays);
            DateTime now = DateTime.Now;

            DateTime orderDate = now.AddDays(randomSpan.Days);

            var user = await animeStockDbContext.Users.FirstAsync(u => u.Id == userId);
            var bookId = bookOrderViewModel.BookInfo.BookId;
            var book = await animeStockDbContext.Books.FindAsync(bookId);
            Order order = new Order()
            {
                UserId = userId,
                OrderDate = orderDate,
                BookId = bookOrderViewModel.BookInfo.BookId,
                Status = (bookOrderViewModel.BookInfo.ReleaseDate > DateTime.Now) ? PreOrder :
                         (orderDate < DateTime.Now) ? Delivered : Ordered,
                TotalPrice = (bookOrderViewModel.UserQuantity == 0) ? bookOrderViewModel.BookInfo.Price 
                : bookOrderViewModel.BookInfo.Price * bookOrderViewModel.UserQuantity,
                FirstName = WebUtility.HtmlEncode(bookOrderViewModel.User.FirstName),
                EmailAddress = WebUtility.HtmlEncode(bookOrderViewModel.User.Email),
                UserOrders = bookOrderViewModel.UserQuantity
            };

            book.Quantity = book.Quantity - bookOrderViewModel.UserQuantity;
            await animeStockDbContext.Orders.AddAsync(order);
            await animeStockDbContext.SaveChangesAsync();
        }

    }
}
