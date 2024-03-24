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

            DateTime releaseDate = bookOrderViewModel.BookInfo.ReleaseDate;
            DateTime orderDate = releaseDate.AddDays(randomSpan.Days);

            var user = await animeStockDbContext.Users.FirstAsync(u => u.Id == userId);
            var book = await animeStockDbContext.Books.FirstAsync(b => b.Id == bookOrderViewModel.BookInfo.BookId);

            Order order = new Order()
            {
                UserId = userId,
                OrderDate = orderDate,
                BookId = bookOrderViewModel.BookInfo.BookId,
                Status = (bookOrderViewModel.BookInfo.ReleaseDate > DateTime.Now) ? PreOrder :
                         (bookOrderViewModel.OrderDate < DateTime.Now) ? Ordered : Delivered,
                TotalPrice = bookOrderViewModel.BookInfo.Price * bookOrderViewModel.BookInfo.UserQuantity,
                FirstName = WebUtility.HtmlEncode(bookOrderViewModel.User.FirstName),
                EmailAddress = WebUtility.HtmlEncode(bookOrderViewModel.User.Email)
            };

            book.Quantity = book.Quantity - bookOrderViewModel.BookInfo.UserQuantity;
            await animeStockDbContext.Orders.AddAsync(order);
            await animeStockDbContext.SaveChangesAsync();
        }

    }
}
