using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.EntityFrameworkCore;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class AdminTagServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IBookTagService bookTagService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            bookTagService = new BookTagService(animeStockDbContext);
        }

        [Test]
        public async Task TestGetBookTags()
        {
            IEnumerable<TagViewModel> expectedBookTags = new List<TagViewModel>()
            {
                new TagViewModel()
                {
                    Id = tag1.Id,
                    Name = tag1.Name,
                },
                new TagViewModel()
                {
                    Id = tag2.Id,
                    Name = tag2.Name,
                },
                new TagViewModel()
                {
                    Id = tag3.Id,
                    Name = tag3.Name,
                },
            };

            IEnumerable<TagViewModel> actualBookTags = await this.bookTagService.GetBookTagsAsync();

            CollectionAssert.AreEqual(expectedBookTags, actualBookTags, new TagViewModelComparator());
        }

        [Test]
        public async Task TestDeleteBookTag()
        {
            tag1.IsDeleted = true;
            animeStockDbContext.SaveChanges();

            Assert.IsTrue(tag1.IsDeleted);
        }

        [Test]
        public async Task TestRecoverBookTag()
        {
            await this.bookTagService.DeleteBookTagByIdAsync(tag1.Id);
            await this.bookTagService.RecoverBookTagByIdAsync(tag1.Id);

            Assert.IsFalse(tag1.IsDeleted);
        }

        [Test]
        public async Task TestGetBookTagToEdit()
        {
            EditBookTagViewModel expectedBookTagViewModel = new EditBookTagViewModel()
            {
                Name = tag1.Name
            };

            EditBookTagViewModel actualBookTag = await this.bookTagService.GetBookTagToEditAsync(tag1.Id);

            Assert.AreEqual(expectedBookTagViewModel.Name, actualBookTag.Name);
        }

        [Test]
        public async Task TestEditBookTag()
        {
            EditBookTagViewModel bookTagViewModel = new EditBookTagViewModel()
            {
                Name = "book tag",
            };

            await this.bookTagService.EditBookTagByIdAsync(tag1.Id, bookTagViewModel);

            Assert.AreEqual(bookTagViewModel.Name, tag1.Name);
        }

        [Test]
        public async Task TestCreateBookTag()
        {
            int currentBookTagsCountInDatabase = animeStockDbContext.Tags.Count();
            int expectedBookTagsCountInDatabase = currentBookTagsCountInDatabase + 1;
            EditBookTagViewModel editBookTagViewModel = new EditBookTagViewModel()
            {
                Name = "new book tag"
            };

            await this.bookTagService.CreateBookTagAsync(editBookTagViewModel);

            Assert.AreEqual(expectedBookTagsCountInDatabase, animeStockDbContext.Tags.Count());
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
