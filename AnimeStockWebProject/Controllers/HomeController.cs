using AnimeStockWebProject.Extensions;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
namespace AnimeStockWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly AnimeStockDbContext animeStockDbContext;

        public HomeController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMemoryCache memoryCache, AnimeStockDbContext animeStockDbContext)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
            this.animeStockDbContext = animeStockDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (this.User?.Identity.IsAuthenticated ?? false)
            {
                if (this.User.IsInRole(AdminRoleName) || this.User.IsInRole(ModeratorRoleName))
                {
                    return this.RedirectToAction("Index", "Home", new {Area = AdminAreaName});
                }
                else
                {
                    if (!await roleManager.RoleExistsAsync(UserRoleName))
                    {
                        IdentityRole<Guid> userRole = new IdentityRole<Guid>(UserRoleName);
                        await roleManager.CreateAsync(userRole);
                    }
                    if (!this.User.IsInRole(UserRoleName))
                    {
                        User user = await userManager.FindByIdAsync(this.User.GetId().ToString());
                        await userManager.AddToRoleAsync(user, UserRoleName);
                        await animeStockDbContext.SaveChangesAsync();
                    }
                }
            }
            ViewBag.ShowFooter = false;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404 || statusCode == 400)
            {
                return this.View("Error404");
            }
            else if (statusCode == 401)
            {
                return View("Unauthorized");
            }

            return View("Error");
        }
    }
}
