using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Comment;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private ICommentService commentService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            commentService = new CommentService(animeStockDbContext);
        }

        [Test]
        public async Task TestCreateComment()
        {
            int expectedCommentCount = 4;

            PostCommentViewModel postCommentViewModel = new PostCommentViewModel()
            {
                Description = "test comment",
                BookId = 3
            };
            Guid userId = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70");
            string userName = "Test User";

            await this.commentService.CreateCommentAsync(postCommentViewModel, userId, userName, false, true);

            Assert.AreEqual(expectedCommentCount, this.animeStockDbContext.Comments.Count());
        }

        [Test]
        public async Task TestCommentExistsReturnTrue()
        {
            bool value = await this.commentService.CheckIfCommentExistsByIdAsync(1);

            Assert.IsTrue(value);
        }
        [Test]
        public async Task TestCommectExistsReturnFalse()
        {
            bool value = await this.commentService.CheckIfCommentExistsByIdAsync(7);

            Assert.IsFalse(value);
        }

        [Test]
        public async Task TestCommentIsDeleted()
        {
            bool value = await this.commentService.DeleteCommentAsync(1);

            Assert.IsTrue(value);
        }

        [Test]
        public async Task TestEditComment()
        {
            string expectedCommentDescription = "Edited comment 1";
            EditCommentViewModel editCommentViewModel = new EditCommentViewModel()
            {
                Description = expectedCommentDescription,
                Id = 1
            };

            await this.commentService.EditCommentAsync(editCommentViewModel);

            Assert.AreEqual(expectedCommentDescription, comment1.Description);
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
