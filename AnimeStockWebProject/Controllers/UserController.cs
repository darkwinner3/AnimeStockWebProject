namespace AnimeStockWebProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AnimeStockWebProject.Core.Contracts;
    using AnimeStockWebProject.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Caching.Memory;
    using static Common.NotificationKeys;
    using static Common.NotifiactionMessages;
    using static Common.GeneralAplicaitonConstants;
    using AnimeStockWebProject.Extensions;
    using AnimeStockWebProject.Core.Models.User;
    using System.Security.Claims;
    using AnimeStockWebProject.Core.Models.Book;

    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMemoryCache memoryCache;
        public UserController(IUserService userService, UserManager<User> userManager,
            SignInManager<User> signInManager, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(Guid id)
        {
            ViewBag.ShowFooter = false;
            if (id != this.User.GetId())
            {
                return Unauthorized();
            }
            try
            {
                string cacheKey = string.Format(UserInfoCacheKey, id);
                UserInfoViewModel userInfoViewModel = this.memoryCache.Get<UserInfoViewModel>(cacheKey);
                if (userInfoViewModel == null)
                {
                    userInfoViewModel = await userService.GetUserInfoByIdAsync(id);
                    MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(UserInfoCacheDuration));
                    this.memoryCache.Set(cacheKey, userInfoViewModel, options);
                }
                return View(userInfoViewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Index", "Home");
            }

           
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserInfoViewModel userInfoViewModel)
        {
            if(userInfoViewModel.ProfilePictureFile != null)
            {
                string fileExtension = Path.GetExtension(userInfoViewModel.ProfilePictureFile.FileName).ToLower();

                if (!string.IsNullOrWhiteSpace(userInfoViewModel.ProfilePicturePath))
                {
                    await userService.DeleteUserProfilePictureAsync(this.User.GetId(), userInfoViewModel.ProfilePicturePath);
                }

                userInfoViewModel.ProfilePicturePath = await userService.UploadUserImageAsync(userInfoViewModel, this.User.GetId());

                try
                {
                    User user = await userManager.FindByIdAsync(this.User.GetId().ToString());
                    await userService.SaveUserInfoAsync(this.User.GetId(), userInfoViewModel);
                    Claim userNameClaim = new Claim("ProfilePicturePath", userInfoViewModel.ProfilePicturePath);

                    if(this.User.HasClaim(c => c.Type == "ProfilePicturePath"))
                    {
                        Claim claim = this.User.Claims.FirstOrDefault(c => c.Type == "ProfilePicturePath");
                        await userManager.RemoveClaimAsync(user, claim);
                    }

                    //TempData[SuccessMessage] = SuccessfullyUpdatedAccount;
                    await userManager.AddClaimAsync(user, userNameClaim);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    this.memoryCache.Remove(string.Format(UserInfoCacheKey, userInfoViewModel.Id));
                    return RedirectToAction("UserProfile", "User");
                }

                catch (Exception)
                {
                    TempData[ErrorMessage] = DefaultErrorMessage;
                    return RedirectToAction("Index", "Home");
                }
            }

            try
            {
                await userService.SaveUserInfoAsync(this.User.GetId(), userInfoViewModel);
                TempData[SuccessMessage] = SuccessfullyUpdatedAccount;
                string cacheKey = string.Format(UserInfoCacheKey, userInfoViewModel.Id);
                this.memoryCache.Remove(cacheKey);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserFavoriteItems(UserFavoritesViewModel userFavoritesViewModel, Guid id)
        {
            if (userFavoritesViewModel.currentPage < 1)
            {
                userFavoritesViewModel.currentPage = 1;
            }

            try
            {
                string cacheKey = string.Format(UserFavoriteItemsCacheKey, id);
                IEnumerable<BookViewModel> userBooks = this.memoryCache.Get<IEnumerable<BookViewModel>>(cacheKey);

                if (userBooks == null)
                {
                    userBooks = await userService.GetUserFavoriteBooksAsync(id);
                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(UserFavoriteItemsCacheDuration));
                    this.memoryCache.Set(cacheKey, userBooks, cacheOptions);
                }
                userFavoritesViewModel.BookViewModels = userBooks;

                return View(userFavoritesViewModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
