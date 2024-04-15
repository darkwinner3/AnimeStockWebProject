using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.IO.Pipes;
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

        public async Task<FileStreamResult> DownloadBook(string filePath)
        {
            string path = Path.GetDirectoryName(filePath);
            string bookFileName = Path.GetFileName(filePath);
            //Local application location CHANGE ON DIFFERENT PC. MUST BE USED FOR LOCAL UNIT TESTS
            //string bookFolderPath = Path.GetFullPath(@"D:\Important Learning\Programming\web projects\AnimeStockWebProject\AnimeStockWebProject\wwwroot");

            //Web application location
            string bookFolderPath = Path.GetFullPath(@"C:\home\site\wwwroot\wwwroot\");
            string fullPath = Path.Join(bookFolderPath, path, bookFileName);
            string contentType = "application/octet-stream";

            FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            
            var result = await Task.Run(() =>
            {
                return new FileStreamResult(fileStream, contentType)
                {
                    FileDownloadName = bookFileName
                };
            });

            return result;
        }

        public async Task<OrderInfoViewModel> GetBookOrderInfoAsync(Guid Id)
        {
            Order orderToFind = await animeStockDbContext.Orders
                .Include(b => b.Book)
                .ThenInclude(b => b.Pictures)
                .FirstAsync(o => o.Id == Id);

            OrderInfoViewModel orderInfoViewModel = new OrderInfoViewModel()
            {
                Id = orderToFind.Id,
                FirstName = orderToFind.FirstName,
                FilePath = orderToFind.Book.FilePath,
                PicturePath = orderToFind.Book.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                Price = orderToFind.TotalPrice,
                Title = orderToFind.Book.Title,
                Email = orderToFind.EmailAddress,
                PrintType = orderToFind.Book.PrintType.ToString()
            };

            return orderInfoViewModel;

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
                         (orderDate < DateTime.Now || bookOrderViewModel.BookInfo.PrintType == "Digital") ? Delivered : Ordered,
                TotalPrice = (bookOrderViewModel.UserQuantity == 0) ? bookOrderViewModel.BookInfo.Price 
                : bookOrderViewModel.BookInfo.Price * bookOrderViewModel.UserQuantity,
                FirstName = WebUtility.HtmlEncode(bookOrderViewModel.User.FirstName),
                EmailAddress = WebUtility.HtmlEncode(bookOrderViewModel.User.Email),
                UserOrders = bookOrderViewModel.UserQuantity
            };
            if (bookOrderViewModel.BookInfo.PrintType == "Digital")
            {
                order.OrderDate = DateTime.Now;
            }
            if (order.Status == PreOrder)
            {
                orderDate = bookOrderViewModel.BookInfo.ReleaseDate.AddDays(randomSpan.Days);
                order.OrderDate = orderDate;
            }

            book.Quantity = book.Quantity - bookOrderViewModel.UserQuantity;
            await animeStockDbContext.Orders.AddAsync(order);
            await animeStockDbContext.SaveChangesAsync();
        }

        //update order periodically
        public async Task UpdateOrderStatusAsync()
        {
            var orders = await animeStockDbContext.Orders.ToArrayAsync();

            foreach (var order in orders)
            {
                if (order.Status == PreOrder && order.Book.ReleaseDate <= DateTime.Now)
                {
                    order.Status = Ordered;
                }
                if (order.Status == Ordered && order.OrderDate <= DateTime.Now)
                {
                    order.Status = Delivered;
                }
            }

            await animeStockDbContext.SaveChangesAsync();
        }

    }
}
