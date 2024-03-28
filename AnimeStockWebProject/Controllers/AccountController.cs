using AnimeStockWebProject.Core.Models.Account;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;

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
            ViewBag.ShowFooter = false;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            if (await animeStockDbContext.Users.AnyAsync(u => u.UserName == registerViewModel.Username))
            {
                ModelState.AddModelError(nameof(registerViewModel.Username), "Username already taken");
                return View(registerViewModel);
            }

            if (await animeStockDbContext.Users.AnyAsync(u => u.Email == registerViewModel.Email))
            {
                ModelState.AddModelError(nameof(registerViewModel.Email), "Email is already used");
                return View(registerViewModel);
            }
            var user = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Username
            };
            var result = await userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                memoryCache.Remove(AdminUsersCacheKey);
                return RedirectToAction(nameof(Login));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.ShowFooter = false;
            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ShowFooter = false;
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (IsValidEmail(loginViewModel.UsernameOrEmail))
            {
                var userByEmail = await userManager.FindByEmailAsync(loginViewModel.UsernameOrEmail);
                if (userByEmail != null)
                {
                    var resultByEmail = await signInManager.PasswordSignInAsync(userByEmail, loginViewModel.Password, false, false);
                    if (resultByEmail.Succeeded)
                    {
                        await signInManager.SignInAsync(userByEmail, isPersistent: true);
                        if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                        {
                            return Redirect(loginViewModel.ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                var userByName = await userManager.FindByNameAsync(loginViewModel.UsernameOrEmail);
                if (userByName != null)
                {
                    var resultByName = await signInManager.PasswordSignInAsync(userByName, loginViewModel.Password, false, false);
                    if (resultByName.Succeeded)
                    {
                        await signInManager.SignInAsync(userByName, isPersistent: true);
                        if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                        {
                            return Redirect(loginViewModel.ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ViewBag.ShowFooter = false;

            ModelState.AddModelError("", "Invalid username, email or password");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<IActionResult> ChangeRole(Guid id, string? role = null)
        {
            User userToFind = await userManager.FindByIdAsync(id.ToString());
            if (!string.IsNullOrWhiteSpace(role))
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);
                    await roleManager.CreateAsync(newRole);
                }
                if (!await userManager.IsInRoleAsync(userToFind, role))
                {
                    await userManager.AddToRoleAsync(userToFind, role);
                    //Saves changes to SeedUsers
                    await animeStockDbContext.SaveChangesAsync();
                }
            }
            else
            {
                if (await userManager.IsInRoleAsync(userToFind, ModeratorRoleName))
                {
                    await userManager.RemoveFromRoleAsync(userToFind, ModeratorRoleName);
                    if (!await roleManager.RoleExistsAsync(UserRoleName))
                    {
                        IdentityRole<Guid> userRole = new IdentityRole<Guid>(UserRoleName);
                        await roleManager.CreateAsync(userRole);
                    }
                    if (!await userManager.IsInRoleAsync(userToFind, UserRoleName))
                    {
                        await userManager.AddToRoleAsync(userToFind, UserRoleName);
                    }
                    //Saves changes to SeedUsers
                    await animeStockDbContext.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
