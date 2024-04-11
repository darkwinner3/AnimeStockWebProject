using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book.DataModels;
using AnimeStockWebProject.Core.Models.Pager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Areas.Admin.Models.User;

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
                ViewBag.ShowFooter = true;
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
            ViewBag.ShowFooter = true;
            return View(bookAddViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookAddViewModel bookAddViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookAddViewModel);
            }
            try
            {
                int bookId = await bookAdminService.BookAddAsync(bookAddViewModel);
                TempData[SuccessMessage] = SuccessfullyAddedBook;
                if (bookAddViewModel.CoverImg != null)
                {
                    await bookAdminService.CreateCoverImgAsync(bookId, bookAddViewModel);
                }
                if (bookAddViewModel.Pictures != null && bookAddViewModel.Pictures.Count > 0)
                {
                    await bookAdminService.CreateBookPicturesAsync(bookId, bookAddViewModel);
                }
                if (bookAddViewModel.BookFile != null)
                {
                    await bookAdminService.CreateBookFileAsync(bookId, bookAddViewModel);
                }
                ViewBag.ShowFooter = true;
                return RedirectToAction("Index", "Book", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                ViewBag.ShowFooter = true;
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Create", "Book", new { Area = AdminAreaName });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int bookId, int bookTypeId)
        {
            try
            {
                BookEditViewModel bookEditViewModel = await bookAdminService.GetBookToEditAsync(bookId);
                bookEditViewModel.BookTags = await bookTagService.GetBookTagsAsync();
                bookEditViewModel.BookTypeId = bookTypeId;
                bookEditViewModel.BookTypes = await bookTypeService.GetAllBookTypesAsync();
                bookEditViewModel.SelectedBookTagIds = bookEditViewModel.currentTags.Select(t => t.Id).ToList();
                ViewBag.ShowFooter = true;
                return View(bookEditViewModel);
            }
            catch (Exception)
            {
                ViewBag.ShowFooter = true;
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Edit", "Book", new { Area = AdminAreaName });
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int bookId, BookEditViewModel bookEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookEditViewModel);
            }
            try
            {
                await bookAdminService.EditBookByIdAsync(bookId, bookEditViewModel);
                TempData[SuccessMessage] = SuccessfullyEditedBook;
                ViewBag.ShowFooter = true;
                return RedirectToAction("Index", "Book", new { Area = AdminAreaName });
            }
            catch (Exception)
            {
                ViewBag.ShowFooter = true;
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Edit", "Book", new { Area = AdminAreaName });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await bookAdminService.DeleteBookByIdAsync(id);
                TempData[SuccessMessage] = SuccessfullyDeletedBook;
                ViewBag.ShowFooter = true;
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Recover(int id)
        {
            try
            {
                await bookAdminService.RecoverBookByIdAsync(id);
                TempData[SuccessMessage] = SuccessfullyRecoveredBook;
                ViewBag.ShowFooter = true;
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }

        private async Task ClearCache()
        {
            IEnumerable<UsersViewModel> users = this.memoryCache.Get<IEnumerable<UsersViewModel>>(AdminUsersCacheKey);
            if (users == null)
            {
                users = await userService.GetAllUsersAsync();
                MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(AdminUsersDuration));
                this.memoryCache.Set(AdminUsersCacheKey, users, memoryCacheEntryOptions);
            }
            foreach (var user in users)
            {
                this.memoryCache.Remove(string.Format(UserFavoriteItemsCacheKey, user.Id));
            }
        }
    }
}
