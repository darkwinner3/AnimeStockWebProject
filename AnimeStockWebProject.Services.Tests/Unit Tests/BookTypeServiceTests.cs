namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    using AnimeStockWebProject.Areas.Admin.Models.BookType;
    using AnimeStockWebProject.Areas.Admin.Services;
    using AnimeStockWebProject.Services.Tests.Comparators;
    using Areas.Admin.Contracts;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using static DatabaseSeeder;

    [TestFixture]
    public class BookTypeServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> DbContextOptions;
        private IBookTypeService bookTypeService;

        [SetUp]
        public void SetUp()
        {
            this.DbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.animeStockDbContext = new AnimeStockDbContext(this.DbContextOptions, false);
            this.animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            bookTypeService = new BookTypeService(animeStockDbContext);
        }

        [Test]
        public async Task TestGetAllAvailableBookTypes1()
        {
            IEnumerable<BookTypeViewModel> expectedBookTypes = new List<BookTypeViewModel>()
            {
                new BookTypeViewModel()
                {
                    Id = bookType1.Id,
                    Name = bookType1.Name,
                },
                new BookTypeViewModel()
                {
                    Id= bookType2.Id,
                    Name = bookType2.Name,
                }
            };

            IEnumerable<BookTypeViewModel> actualBookTypes = await this.bookTypeService.GetAllBookTypesAsync();

            CollectionAssert.AreEqual(expectedBookTypes, actualBookTypes, new BookTypeViewModelComparator());
        }

        [Test]
        public async Task TestGetAllAvailableBookTypes2()
        {
            bookType2.IsDeleted = true;
            animeStockDbContext.SaveChanges();
            IEnumerable<BookTypeViewModel> expectedBookTypes = new List<BookTypeViewModel>()
            {
                new BookTypeViewModel()
                {
                    Id = bookType1.Id,
                    Name = bookType1.Name,
                },
                new BookTypeViewModel()
                {
                    Id = bookType2.Id,
                    Name = bookType2.Name,
                }
            };
            IEnumerable<BookTypeViewModel> actualBookTypes = await this.bookTypeService.GetAllBookTypesAsync();

            CollectionAssert.AreEqual(expectedBookTypes, actualBookTypes, new BookTypeViewModelComparator());
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
