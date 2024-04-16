using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Core.Models.User;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    using static DatabaseSeeder;
    [TestFixture]
    public class BookAdminServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private IWebHostEnvironment env;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IBookAdminService bookAdminService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(env => env.WebRootPath).Returns("fake/web/root/path");
            env = mockEnv.Object;
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            bookAdminService = new BookAdminService(animeStockDbContext, env);
        }

        [Test]
        public async Task TestBookAdd()
        {
            int currentBookCount = animeStockDbContext.Books.Count();
            int expectedBookCount = currentBookCount + 1;

            var memoryStream = new MemoryStream(new byte[0]);
            var coverImgMock = new Mock<IFormFile>();
            coverImgMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask)
                .Callback((Stream stream, CancellationToken token) => memoryStream.CopyTo(stream));

            var bookFileMock = new Mock<IFormFile>();
            bookFileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask)
                .Callback((Stream stream, CancellationToken token) => memoryStream.CopyTo(stream));

            var mockFiles = new List<IFormFile>();
            var formFileCollection = new Mock<IFormFileCollection>();
            formFileCollection.Setup(f => f.GetEnumerator()).Returns(mockFiles.GetEnumerator());

            for (int i = 0; i < 2; i++)
            {
                var fileMock = new Mock<IFormFile>();
                fileMock.Setup(f => f.FileName).Returns($"test{i}.txt");

                mockFiles.Add(fileMock.Object);
            }

            BookAddViewModel viewModel = new BookAddViewModel()
            {
                Author = "test author",
                Title = "Title",
                Description = "Description",
                ReleaseDate = DateTime.Now,
                Illustrator = "test illustrator",
                Pages = 255,
                Price = 5.55m,
                Publisher = "test publisher",
                PrintType = Infrastructure.Data.Enums.PrintTypeEnum.Phisycal,
                Quantity = 20,
                SelectedBookTagIds = new List<int> { 2, 3 },
                BookTypeId = 1,
                CoverImg = coverImgMock.Object,
                BookFile = bookFileMock.Object,
                Pictures = formFileCollection.Object,
            };

            await this.bookAdminService.BookAddAsync(viewModel);

            Assert.AreEqual(expectedBookCount, animeStockDbContext.Books.Count());
        }

        [Test]
        public async Task TestGetAllBooks()
        {
            IEnumerable<BookViewModel> expectedBooks = new List<BookViewModel>()
            {
                new BookViewModel()
                {
                    Title = book1.Title,
                    Id = book1.Id,
                    BookTypeId = book1.BookTypeId,
                    PictureUrl = book1.Pictures.FirstOrDefault(p => p.Path.Contains("cover")).Path
                },
                new BookViewModel()
                {
                    Title = book2.Title,
                    Id = book2.Id,
                    BookTypeId = book2.BookTypeId,
                    PictureUrl = book2.Pictures.FirstOrDefault(p => p.Path.Contains("cover")).Path
                },
                new BookViewModel()
                {
                    Title = book3.Title,
                    Id = book3.Id,
                    BookTypeId = book3.BookTypeId,
                    PictureUrl = book3.Pictures.FirstOrDefault(p => p.Path.Contains("cover")).Path
                },
            };

            IEnumerable<BookViewModel> actualBooks = await this.bookAdminService.GetAllBooksAsync(new Pager(6, 1, 6));

            CollectionAssert.AreEqual(expectedBooks, actualBooks, new AdminBookViewModelComparator());
        }

        [Test]
        public async Task TestGetAllBooksCount()
        {
            int expectedCount = 3;

            int actualCount = await this.bookAdminService.GetAllBooksCountAsync();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public async Task GetBookToEdit()
        {
            var expectedBookId = book1.Id;

            var actualBookId = await this.bookAdminService.GetBookToEditAsync(expectedBookId);

            Assert.AreEqual(expectedBookId, actualBookId.Id);
        }

        [Test]
        public async Task TestEditBookById()
        {
            BookEditViewModel viewModel = new BookEditViewModel()
            {
                Author = "test author",
                Title = "Title",
                Description = "Description",
                ReleaseDate = DateTime.Now,
                Illustrator = "test illustrator",
                Pages = 255,
                Price = 5.55m,
                Publisher = "test publisher",
                PrintType = Infrastructure.Data.Enums.PrintTypeEnum.Phisycal,
                Quantity = 20,
                SelectedBookTagIds = new List<int> { 2, 3 },
                BookTypeId = 2
            };

            await this.bookAdminService.EditBookByIdAsync(book1.Id, viewModel);

            Assert.AreEqual(viewModel.Author, book1.Author);
            Assert.AreEqual(viewModel.Title, book1.Title);
            Assert.AreEqual(viewModel.Description, book1.Description);
            Assert.AreEqual(viewModel.ReleaseDate, book1.ReleaseDate);
            Assert.AreEqual(viewModel.Illustrator, book1.Illustrator);
            Assert.AreEqual(viewModel.Pages, book1.Pages);
            Assert.AreEqual(viewModel.Price, book1.Price);
            Assert.AreEqual(viewModel.Publisher, book1.Publisher);
            Assert.AreEqual(viewModel.PrintType, book1.PrintType);
        }

        [Test]
        public async Task TestDeleteBook()
        {
            book1.IsDeleted = true;
            animeStockDbContext.SaveChanges();

            Assert.IsTrue(book1.IsDeleted);
        }

        [Test]
        public async Task TestRecoverBook()
        {
            await this.bookAdminService.DeleteBookByIdAsync(book1.Id);
            await this.bookAdminService.RecoverBookByIdAsync(book1.Id);

            Assert.IsFalse(book1.IsDeleted);
        }

        [Test]
        public async Task TestDeleteBooksJobAsync()
        {
            int expectedBookCount = await this.animeStockDbContext.Books.CountAsync() - 1;
            book1.IsDeleted = true;
            this.animeStockDbContext.SaveChanges();

            await this.bookAdminService.DeleteBooksJobAsync();

            int actualBookCount = await this.animeStockDbContext.Books.CountAsync();
            Assert.AreEqual(expectedBookCount, actualBookCount);
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
