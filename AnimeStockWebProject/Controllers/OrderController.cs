using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;

namespace AnimeStockWebProject.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IBookService bookService;

        public OrderController(IUserService userService, IOrderService orderService, IBookService bookService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.bookService = bookService;
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
    }
}
