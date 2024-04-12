using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class PictureServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private IWebHostEnvironment env;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IPictureAdminService pictureAdminService;

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
            pictureAdminService = new PictureAdminService(animeStockDbContext, env);
        }

        [Test]
        public async Task TestPictureIsDeleted()
        {
            await this.pictureAdminService.DeletePictureAsync(1);
            bool result = await this.pictureAdminService.PictureIsAlreadyDeletedAsync(1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestPictureExistsTrue()
        {
            bool result = await this.pictureAdminService.PictureExistsByIdAsync(1);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task TestPictureExistsFalse()
        {
            bool result = await this.pictureAdminService.PictureExistsByIdAsync(7);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestCheckPictureRecoveredTrue()
        {
            bool result = await this.pictureAdminService.PictureIsRecoveredAsync(1);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task TestCheckPictureRecoveredFalse()
        {
            await this.pictureAdminService.DeletePictureAsync(1);
            bool result = await this.pictureAdminService.PictureIsRecoveredAsync(1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task TestRecoverPicture()
        {
            await this.pictureAdminService.DeletePictureAsync(1);
            await this.pictureAdminService.RecoverPictureAsync(1);

            bool result = await this.pictureAdminService.PictureIsRecoveredAsync(1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestPictureIsFullyDeleted()
        {
            await this.pictureAdminService.DeletePictureAsync(1);
            animeStockDbContext.SaveChanges();
            var expectedPictures = animeStockDbContext.Pictures.Count() - 1;

            await this.pictureAdminService.DeletePicturesAsync();
            animeStockDbContext.SaveChanges();

            var actualPictures = animeStockDbContext.Pictures.Count();

            Assert.AreEqual(actualPictures, expectedPictures);
        }



        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
