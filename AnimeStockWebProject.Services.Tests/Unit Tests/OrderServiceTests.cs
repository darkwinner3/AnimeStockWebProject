using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Core.Models.User;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;
namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IOrderService orderService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            orderService = new OrderService(animeStockDbContext);
        }

        [Test]
        public async Task TestOrderBook()
        {
            int expectedBookOrdersCount = 3;

            BookOrderDetailsViewModel bookOrderViewModel = new BookOrderDetailsViewModel()
            {
                BookInfo = new BookOrderViewModel
                {
                    BookId = book3.Id,
                    ReleaseDate = book3.ReleaseDate,
                    Price = book3.Price,
                    PrintType = book3.PrintType.ToString(),
                    Quantity = book3.Quantity,
                    Title = book3.Title,
                },
                User = new UserViewModel
                {
                    Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                    FirstName = "Test User",
                    Email = "testuser123@gmail.com",
                },
            };

            await this.orderService.OrderBookAsync(bookOrderViewModel, bookOrderViewModel.User.Id);

            Assert.AreEqual(expectedBookOrdersCount, book3.Orders.Count());
        }

        [Test]
        public async Task TestBookOrderInfo()
        {
            OrderInfoViewModel expectedOrderInfo = new OrderInfoViewModel()
            {
                Price = book2.Price * 4,
                PrintType = book2.PrintType.ToString(),
                Title = book2.Title,
                Id = Guid.Parse("0f8e1ca6-b0aa-47f6-8284-666650c464d0"),
                FirstName = "Alice",
                Email = "alice@gmail.com",
                PicturePath = "/cover/test/path",
                FilePath = "/Books/Date A Live/Date A Live, Vol. 2_ Puppet Yoshino.pdf",
            };

            OrderInfoViewModel actualOrderInfo = await this.orderService.GetBookOrderInfoAsync(expectedOrderInfo.Id);

            var comparator = new OrderInfoViewModelComperator();

            int comparisonResult = comparator.Compare(expectedOrderInfo, actualOrderInfo);

            Assert.AreEqual(0, comparisonResult);
        }

        [Test]
        public async Task TestUpdateOrderStatus()
        {
            var order = await animeStockDbContext.Orders.FirstAsync();
            order.Status = Infrastructure.Data.Enums.StatusEnum.PreOrder;
            order.OrderDate = DateTime.Now;
            await animeStockDbContext.SaveChangesAsync();

            await orderService.UpdateOrderStatusAsync();

            Assert.IsTrue(order.Status == Infrastructure.Data.Enums.StatusEnum.Delivered);
        }

        [Test]
        public async Task TestDownloadBook()
        {
            var order = await animeStockDbContext.Orders.FirstAsync(o => o.Book.PrintType == Infrastructure.Data.Enums.PrintTypeEnum.Digital);

            string filePath = order.Book.FilePath;

            var result = await orderService.DownloadBook(filePath);

            Assert.IsNotNull(result);

        }


        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
