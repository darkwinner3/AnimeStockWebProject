using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.User;
using AnimeStockWebProject.Areas.Admin.Services;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using AnimeStockWebProject.Services.Tests.Comparators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimeStockWebProject.Services.Tests.DatabaseSeeder;

namespace AnimeStockWebProject.Services.Tests.Unit_Tests
{
    [TestFixture]
    public class UserAdminServiceTests
    {
        private AnimeStockDbContext animeStockDbContext;
        private DbContextOptions<AnimeStockDbContext> dbContextOptions;
        private IUserAdminService userAdminService;

        [SetUp]
        public void SetUp()
        {
            dbContextOptions = new DbContextOptionsBuilder<AnimeStockDbContext>()
                .UseInMemoryDatabase("AnimeStockSystemInMemory" + Guid.NewGuid().ToString())
                .Options;
            animeStockDbContext = new AnimeStockDbContext(dbContextOptions, false);
            animeStockDbContext.Database.EnsureCreated();
            SeedDatabase(animeStockDbContext);
            userAdminService = new UserAdminService(animeStockDbContext);
        }

        [Test]
        public async Task TestGetAllUsers()
        {
            IEnumerable<UsersViewModel> expectedUsers = new List<UsersViewModel>()
            {
                new UsersViewModel()
                {
                    Id = Guid.Parse("b9a4d407-7518-4aea-a72d-b94c7e389b70"),
                    UserName = "Test User",
                    FirstName = null,
                    Email = "testuser123@gmail.com",
                    Joined = DateTime.Now,

                },

                 new UsersViewModel()
                {
                    Id = Guid.Parse("ee8ddd02-ce94-4f77-8608-819b08dbbb32"),
                    UserName = "Admin",
                    Email = "admin123@gmail.com",
                    FirstName = null,
                    Joined = DateTime.Now,
                }

            };

            IEnumerable<UsersViewModel> actualUsers = await userAdminService.GetAllUsersAsync();

            CollectionAssert.AreEqual(expectedUsers, actualUsers, new UsersViewModelComperator());
        }

        [TearDown]
        public void TearDown()
        {
            animeStockDbContext.Database.EnsureDeleted();
            animeStockDbContext.Dispose();
        }
    }
}
