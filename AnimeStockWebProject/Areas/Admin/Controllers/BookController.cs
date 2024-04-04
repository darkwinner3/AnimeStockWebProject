using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book.DataModels;
using AnimeStockWebProject.Core.Models.Pager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
using AnimeStockWebProject.Areas.Admin.Models.Book;

namespace AnimeStockWebProject.Areas.Admin.Controllers
{
    public class BookController : AdminController
    {
        private readonly IMemoryCache memoryCache;
        private readonly IUserAdminService userService;
        private readonly IBookAdminService bookAdminService;
        private readonly IBookTagService bookTagService;
        private readonly IBookTypeService bookTypeService;

        public BookController(IMemoryCache memoryCache, IUserAdminService userService, IBookAdminService bookAdminService, IBookTagService bookTagService,
            IBookTypeService bookTypeService)
        {
            this.memoryCache = memoryCache;
            this.userService = userService;
            this.bookAdminService = bookAdminService;
            this.bookTagService = bookTagService;
            this.bookTypeService = bookTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0)
            {
               page = 1;
            }

            try
            {
                int totalBooks = await bookAdminService.GetAllBooksCountAsync();

                Pager pager = new Pager(totalBooks, page, AdminBooksPageSize);
                BookDataModel bookDataModel = new BookDataModel()
                {
                    BookViewModels = await bookAdminService.GetAllBooksAsync(pager),
                    Pager = pager
                };
                return View(bookDataModel);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Dashboard", "Home", new { Area = AdminAreaName });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BookAddViewModel bookAddViewModel = new BookAddViewModel();
            bookAddViewModel.BookTags = await bookTagService.GetBookTagsAsync();
            bookAddViewModel.BookTypes = await bookTypeService.GetAllBookTypesAsync();

            return View(bookAddViewModel);
        }
    }
}
