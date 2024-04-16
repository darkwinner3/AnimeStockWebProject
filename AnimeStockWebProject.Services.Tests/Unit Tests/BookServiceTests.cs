using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    using static DatabaseSeeder;
    [TestFixture]
    public class BookServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IBookService bookService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            bookService = new BookService(animeStockDbContext);
        }

        [Test]
        public async Task TestBookExistsTrue()
        {
            bool result = await this.bookService.BookExistsAsync(book1.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestBookExistsFalse()
        {
            bool result = await this.bookService.BookExistsAsync(20);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestBookCountTrue()
        {
            BookQueryViewModel bookQueryViewModel = new BookQueryViewModel()
            {
                SelectedBookTypeIds = new List<int>() { 1 },
                SelectedTagIds = new List<int>() { 2 },
                PrintType = Infrastructure.Data.Enums.PrintTypeEnum.Phisycal,
                BookSortEnum = Core.Models.Book.Enum.BookSortEnum.ByAToZ,
            };

            int expectedResult = 1;
            int actualResult = await bookService.GetCountAsync(bookQueryViewModel);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task TestGetBookByTitleEqual()
        {
            IEnumerable<BookNameViewModel> expectedBooks = new List<BookNameViewModel>()
            {
                new BookNameViewModel()
                {
                    Id = book2.Id,
                    Price = book2.Price,
                    Title = book2.Title,
                }
            };

            IEnumerable<BookNameViewModel> actualBooks = await this.bookService.GetBookByTitleAsync(book1.Title, book1.Id);

            CollectionAssert.AreEqual(expectedBooks, actualBooks, new BookNameViewModelComparator());
        }

        [Test]
        public async Task TestGetBookByTitleNotEqual()
        {
            IEnumerable<BookNameViewModel> expectedBooks = new List<BookNameViewModel>()
            {
                new BookNameViewModel()
                {
                    Id = book3.Id,
                    Price = book3.Price,
                    Title = book3.Title,
                }
            };

            IEnumerable<BookNameViewModel> actualBooks = await this.bookService.GetBookByTitleAsync(book1.Title, book1.Id);

            CollectionAssert.AreNotEqual(expectedBooks, actualBooks, new BookNameViewModelComparator());
        }

        [Test]
        public async Task TestGetAllBooksSortedDataModel()
        {
            AllBooksSortedDataModel expectedBooks = new AllBooksSortedDataModel()
            {
                Books = new List<BookViewModel>()
                {
                    new BookViewModel()
                    {
                        Id = book1.Id,
                        Price = book1.Price,
                        Title = book1.Title,
                        BookType = book1.BookType.Name,
                        ReleaseDate = book1.ReleaseDate,
                        Description = book1.Description,
                        Illustrator = book1.Illustrator,
                        Author = book1.Author,
                    },
                    new BookViewModel()
                    {
                        Id = book2.Id,
                        Price = book2.Price,
                        Title = book2.Title,
                        BookType = book2.BookType.Name,
                        ReleaseDate = book2.ReleaseDate,
                        Description = book2.Description,
                        Illustrator = book2.Illustrator,
                        Author = book2.Author,
                    }
                }
            };

            BookQueryViewModel bookQueryViewModel = new BookQueryViewModel()
            {
                PrintType = Infrastructure.Data.Enums.PrintTypeEnum.Phisycal,
                SelectedTagIds = new List<int>() { 1 },
                BookSortEnum = Core.Models.Book.Enum.BookSortEnum.ByOldestBook,
                Pager = new Pager(3, 1, 5)
            };

            AllBooksSortedDataModel actualBooks = await bookService.GetAllBooksSortedDataModelAsync(Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"), bookQueryViewModel);

            CollectionAssert.AreEqual(expectedBooks.Books, actualBooks.Books, new BookViewModelComparator());
        }

        [Test]
        public async Task TestAddItemToFavorites()
        {
            int expectedFavoriteItems = 3;

            await this.bookService.AddItemToFavorites(3, Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"));

            Assert.AreEqual(expectedFavoriteItems, this.animeStockDbContext.FavoriteProducts.Count());
        }

        [Test]
        public async Task TestRemoveItemFromFavorites()
        {
            int expectedFavoriteItems = 1;

            await this.bookService.RemoveItemFromFavorites(2, Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"));

            Assert.AreEqual(expectedFavoriteItems, this.animeStockDbContext.FavoriteProducts.Count());
        }

        [Test]
        public async Task TestGetBookById()
        {
            BookInfoViewModel bookInfoViewModel = new BookInfoViewModel()
            {
                Id = book1.Id,
                Description = book1.Description,
                ReleaseDate = book1.ReleaseDate,
                Author = book1.Author,
                Illustrator = book1.Illustrator,
                Pages = book1.Pages,
                Price = book1.Price,
                Publisher = book1.Publisher,
                Title = book1.Title,
                Quantity = book1.Quantity,
            };

            BookInfoViewModel actualBookInfoViewModel = await this.bookService.GetBookByIdAsync(bookInfoViewModel.Id, new Pager(3, 1, 5), Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"));

            Assert.AreEqual(bookInfoViewModel.Id, actualBookInfoViewModel.Id);
        }

        [Test]
        public async Task TestGetBookCommentsCount()
        {
            int expectedCommentsCount = 2;

            int actualCommentsCount = await this.bookService.GetBookCommentsCountAsync(book1.Id);

            Assert.AreEqual(expectedCommentsCount, actualCommentsCount);
        }

        [Test]
        public async Task TestGetBookFileNotNull()
        {
            var filePath = book1.FilePath;
            var pageCount = 30;

            var result = await this.bookService.GetBookFileAsync(filePath, pageCount);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [Test]
        public async Task TestGetBookFileNull()
        {
            var filePath = "test/path";
            var pageCount = 30;

            var result = await this.bookService.GetBookFileAsync(filePath, pageCount);

            Assert.IsNull(result);
        }

        [Test]
        public async Task TestGetBookToOrder()
        {
            BookOrderViewModel expectedResult = new BookOrderViewModel()
            {
                BookId = book1.Id,
                ReleaseDate = book1.ReleaseDate,
                Price = book1.Price,
                PrintType = book1.PrintType.ToString(),
                Quantity = book1.Quantity,
                Title = book1.Title,
                UserQuantity = 2
            };

            BookInfoViewModel book = new BookInfoViewModel()
            {
                Id = book1.Id,
                ReleaseDate = book1.ReleaseDate,
                Price = book1.Price,
                PrintType = book1.PrintType.ToString(),
                Quantity = book1.Quantity,
                Title = book1.Title,
                UserQuantity = 2,
            };

            BookOrderViewModel actualResult = await this.bookService.GetBookToOrder(book);

            Assert.AreEqual(expectedResult.BookId, actualResult.BookId);
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
