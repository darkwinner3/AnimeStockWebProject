using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models;
using AnimeStockWebProject.Areas.Admin.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
using static AnimeStockWebProject.Common.NotificationKeys;
using static AnimeStockWebProject.Common.NotifiactionMessages;

namespace AnimeStockWebProject.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserAdminService userService;

        public HomeController(IMemoryCache memoryCache, IUserAdminService userService)
        {
            this.memoryCache = memoryCache;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                AdminPageViewModel adminPageViewModel = this.memoryCache.Get<AdminPageViewModel>(AdminDashBoardCacheKey);
                if (adminPageViewModel == null)
                {
                    adminPageViewModel = new AdminPageViewModel();
                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(AdminDashBoardCacheDuration));
                    this.memoryCache.Set(AdminDashBoardCacheKey, adminPageViewModel, cacheEntryOptions);
                }

                IEnumerable<UsersViewModel> users = this.memoryCache.Get<IEnumerable<UsersViewModel>>(AdminUsersCacheKey);
                if (users == null)
                {
                    users = await userService.GetAllUsersAsync();
                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(AdminUsersDuration));
                    this.memoryCache.Set(AdminUsersCacheKey, users, cacheEntryOptions);
                }
                adminPageViewModel.Users = users;
                return View(adminPageViewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Dashboard", "Home");
            }
        }
    }
}
