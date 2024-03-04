using AnimeStockWebProject.Core.Models.Account;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AnimeStockWebProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IMemoryCache memoryCache;
        //To save the changes on SeedUsers
        private readonly AnimeStockDbContext animeStockDbContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager, AnimeStockDbContext animeStockDbContext,
            IMemoryCache memoryCache)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.animeStockDbContext = animeStockDbContext;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }
    }
}
