using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;

namespace AnimeStockWebProject.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IBookService bookService;
        private readonly IMemoryCache memoryCache;

        public OrderController(IUserService userService, IOrderService orderService, IBookService bookService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.bookService = bookService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> OrderItem(BookInfoViewModel bookInfoViewModel)
        {
            if (!await bookService.BookExistsAsync(bookInfoViewModel.Id))
            {
                return NotFound();
            }

            try
            {
                BookOrderDetailsViewModel bookOrder = new BookOrderDetailsViewModel()
                {
                    User = await userService.GetUserByIdAsync(this.User.GetId()),
                    BookInfo = await bookService.GetBookToOrder(bookInfoViewModel)
                };

                return View(bookOrder);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> OrderItem(BookOrderDetailsViewModel bookOrderDetailsViewModel)
        {
            var validationResults = bookOrderDetailsViewModel.Validate(new ValidationContext(bookOrderDetailsViewModel));
            var userId = this.User.GetId();
            bookOrderDetailsViewModel.User.Id = userId;

            if (!ModelState.IsValid && validationResults != null)
            {
               foreach (var memberName in validationResults.MemberNames)
                {
                    ModelState.AddModelError(memberName, validationResults.ErrorMessage);
                }
               return View(bookOrderDetailsViewModel);
            }
            try
            {
                await orderService.OrderBookAsync(bookOrderDetailsViewModel, userId);
                TempData[SuccessMessage] = SuccessfullyBoughtBook;
                this.memoryCache.Remove(HomePageCacheKey);
                this.memoryCache.Remove(AdminDashBoardCacheKey);
                this.memoryCache.Remove(string.Format(UserOrdersCacheKey, userId));
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
