using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.EntityFrameworkCore;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class TagServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private ITagService tagService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            tagService = new TagService(animeStockDbContext);
        }

        [Test]
        public async Task TestGetAllTags()
        {
            IEnumerable<TagViewModel> expectedTags = new List<TagViewModel>()
            {
                new TagViewModel()
                {
                   Id = 1,
                   Name = "Action"
                },
                new TagViewModel()
                {
                    Id = 2,
                    Name = "Adventure"
                },
                new TagViewModel()
                {
                    Id = 3,
                    Name = "Comedy"
                }
            };

            IEnumerable<TagViewModel> actualTags = await this.tagService.GetAllTagsAsync();
            CollectionAssert.AreEqual(expectedTags, actualTags, new TagViewModelComparator());
        }

        [Test]
        public async Task TestGetAllTagsWithDeleted()
        {
            tag2.IsDeleted = true;
            this.animeStockDbContext.SaveChanges();

            IEnumerable<TagViewModel> expectedTags = new List<TagViewModel>()
            {
                new TagViewModel()
                {
                   Id = 1,
                   Name = "Action"
                },
                new TagViewModel()
                {
                    Id = 3,
                    Name = "Comedy"
                }
            };

            IEnumerable<TagViewModel> actualTags = await this.tagService.GetAllTagsAsync();
            CollectionAssert.AreEqual(expectedTags, actualTags, new TagViewModelComparator());
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
