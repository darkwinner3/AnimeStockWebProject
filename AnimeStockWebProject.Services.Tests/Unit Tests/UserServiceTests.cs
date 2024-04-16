using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Core.Models.User;
using AnimeStockWebProject.Core.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Enums;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    public class UserServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private IWebHostEnvironment env;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IUserService userService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(env => env.WebRootPath).Returns("D:\\Important Learning\\Programming\\web projects\\AnimeStockWebProject\\AnimeStockWebProject\\wwwroot\\");
            env = mockEnv.Object;
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            userService = new UserService(animeStockDbContext, env);
        }

        [Test]
        public async Task TestGetUser()
        {
            UserViewModel expectedUser = new UserViewModel()
            {
                Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                FirstName = "",
                Email = "testuser123@gmail.com",
                PhoneNumber = null
            };

            UserViewModel actualUser = await this.userService.GetUserByIdAsync(expectedUser.Id);

            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
        }

        [Test]
        public async Task TestGetUserInfo()
        {
            UserInfoViewModel userInfoViewModel = new UserInfoViewModel()
            {
                Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                FirstName = "",
                Email = "testuser123@gmail.com",
                PhoneNumber = null
            };

            UserInfoViewModel actualUserInfo = await this.userService.GetUserInfoByIdAsync(userInfoViewModel.Id);

            Assert.AreEqual(userInfoViewModel.FirstName, actualUserInfo.FirstName);
            Assert.AreEqual(userInfoViewModel.Email, actualUserInfo.Email);
            Assert.AreEqual(userInfoViewModel.PhoneNumber, actualUserInfo.PhoneNumber);
        }

        [Test]
        public async Task TestSaveUserInfo()
        {
            UserInfoViewModel userInfoViewModel = new UserInfoViewModel()
            {
                Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                FirstName = "Update Name",
                Email = "testuser123@gmail.com",
                PhoneNumber = null
            };

            await this.userService.SaveUserInfoAsync(userInfoViewModel.Id, userInfoViewModel);

            UserInfoViewModel updateUserInfo = await this.userService.GetUserInfoByIdAsync(userInfoViewModel.Id);
            Assert.AreEqual(userInfoViewModel.FirstName, updateUserInfo.FirstName);
        }

        [Test]
        public async Task TestUploadUserImg()
        {
            var userId = Guid.NewGuid();
            var userInfoViewModel = new UserInfoViewModel()
            {
                ProfilePictureFile = new Mock<IFormFile>().Object
            };

            var memoryStream = new MemoryStream(new byte[0]);
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None))
                .Returns(Task.CompletedTask)
                .Callback((Stream stream, CancellationToken token) => memoryStream.CopyTo(stream));

            userInfoViewModel.ProfilePictureFile = fileMock.Object;

            string path = await userService.UploadUserImageAsync(userInfoViewModel, userId);
            path = path.Replace(Path.DirectorySeparatorChar, '/');

            Assert.AreEqual("img/ProfilePictures/" + userId.ToString() + "_" + userInfoViewModel.ProfilePictureFile.FileName, path);
        }

        [Test]
        public async Task TestUserFavoriteBooks()
        {
            IEnumerable<BookViewModel> expectedFavoriteBooks = new List<BookViewModel>()
            {
                new BookViewModel()
                {
                    Id = book1.Id,
                    Title = book1.Title,
                    Author = book1.Author,
                    Illustrator = book1.Illustrator,
                    Description = book1.Description,
                    BookType = book1.BookType.Name,
                    ReleaseDate = book1.ReleaseDate.Date,
                    PrintType = book1.PrintType.ToString(),
                    Price = book1.Price
                },
                new BookViewModel()
                {
                    Id = book2.Id,
                    Title = book2.Title,
                    Author = book2.Author,
                    Illustrator = book2.Illustrator,
                    Description = book2.Description,
                    BookType = book2.BookType.Name,
                    ReleaseDate = book2.ReleaseDate.Date,
                    PrintType = book2.PrintType.ToString(),
                    Price = book2.Price
                }
            };

            IEnumerable<BookViewModel> actualFavoriteBooks = await this.userService.GetUserFavoriteBooksAsync(Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"));

            CollectionAssert.AreEqual(expectedFavoriteBooks, actualFavoriteBooks, new BookViewModelComparator());
        }

        [Test]
        public async Task TestUserFavoriteBooks2()
        {
            book1.IsDeleted = true;
            animeStockDbContext.SaveChanges();

            IEnumerable<BookViewModel> expectedFavoriteBooks = new List<BookViewModel>()
            {
                new BookViewModel()
                {
                    Id = book2.Id,
                    Title = book2.Title,
                    Author = book2.Author,
                    Illustrator = book2.Illustrator,
                    Description = book2.Description,
                    BookType = book2.BookType.Name,
                    ReleaseDate = book2.ReleaseDate.Date,
                    PrintType = book2.PrintType.ToString(),
                    Price = book2.Price
                }
            };

            IEnumerable<BookViewModel> actualFavoriteBooks = await this.userService.GetUserFavoriteBooksAsync(Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"));

            CollectionAssert.AreEqual(expectedFavoriteBooks, actualFavoriteBooks, new BookViewModelComparator());
        }

        [Test]
        public async Task TestUserOrders()
        {
            IEnumerable<UserOrderViewModel> expectedUserOrders = new List<UserOrderViewModel>()
            {
                new UserOrderViewModel()
                {
                    Id = Guid.Parse("90e61ca6-b0aa-47f6-8284-666650c464d1"),
                    Status = StatusEnum.Delivered.ToString(),
                    Price = 6.54m,
                    UserQuantity = 0,
                    OrderDate = DateTime.Parse("2024-07-10"),
                }
            };

            IEnumerable<UserOrderViewModel> actualUserOrders = await this.userService.GetUserOrdersAsync(Guid.Parse("b96cfcbf-d64d-4a38-9fc8-6cf44e9f81e7"));

            CollectionAssert.AreEqual(expectedUserOrders, actualUserOrders, new UserOrderViewModelComparator());
        }

        [Test]
        public async Task TestRemoveProfilePicture()
        {
            UserInfoViewModel userInfoViewModel = new UserInfoViewModel()
            {
                Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                FirstName = "",
                Email = "testuser123@gmail.com",
                PhoneNumber = null,
                ProfilePicturePath = "test"
            };

            await this.userService.SaveUserInfoAsync(userInfoViewModel.Id, userInfoViewModel);
            await this.userService.DeleteUserProfilePictureAsync(userInfoViewModel.Id, "");

            UserInfoViewModel updatedInfo = await this.userService.GetUserInfoByIdAsync(userInfoViewModel.Id);
            
            Assert.IsNull(updatedInfo.ProfilePicturePath);
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
