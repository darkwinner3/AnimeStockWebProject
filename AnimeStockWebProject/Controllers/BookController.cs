using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.BookType;
using AnimeStockWebProject.Core.Models.Pager;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;
using AnimeStockWebProject.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Controllers
{
    using static Common.GeneralAplicaitonConstants;
    public class BookController : Controller
    {
        private readonly ITagService tagService;
        private readonly IBookService bookService;
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeService typeService;
        public BookController(ITagService tagService, IBookService bookService,
            IMemoryCache memoryCache, ITypeService typeService, IOrderService orderService)
        {
            this.tagService = tagService;
            this.bookService = bookService;
            this.memoryCache = memoryCache;
            this.typeService = typeService;
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Books([FromQuery] BookQueryViewModel bookQueryViewModel, int bookTypeId)
        {
            if (bookQueryViewModel.currentPage < 1)
            {
                bookQueryViewModel.currentPage = 1;
            }


            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.GetId();
            }

            
            
            IEnumerable<TagViewModel> tags = this.memoryCache.Get<IEnumerable<TagViewModel>>(BookTagsCacheKey);
            if (tags == null)
            {
                tags = await tagService.GetAllTagsAsync();
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(BookTagsCacheDuration));
                this.memoryCache.Set(BookTagsCacheKey, tags, cacheOptions);
            }

            IEnumerable<BookTypeViewModel> types = this.memoryCache.Get<IEnumerable<BookTypeViewModel>>(BookTypesCacheKey);
            if(types == null)
            {
                types = await typeService.GetAllTypesAsync();
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(BookTypesCacheDuration));
                this.memoryCache.Set(BookTypesCacheKey, types, cacheOptions);
            }

            bookQueryViewModel.BookTypes = types;
            bookQueryViewModel.BookTags = tags;

            var selectedBookTypeId = types.FirstOrDefault(t => t.Id == bookTypeId);

            if (selectedBookTypeId != null)
            {
                bookQueryViewModel.SelectedBookTypeIds = new List<int> { selectedBookTypeId.Id };
            }

            Pager pager = new Pager(await bookService.GetCountAsync(bookQueryViewModel), bookQueryViewModel.currentPage, BooksPageSize);
            bookQueryViewModel.Pager = pager;
            AllBooksSortedDataModel sortedBooks = await bookService.GetAllBooksSortedDataModelAsync(userId, bookQueryViewModel);
            bookQueryViewModel.BookViewModels = sortedBooks.Books;

            return View(bookQueryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id, int page = 1)
        {
            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.GetId();
            }

            if (page <= 0)
            {
                page = 1;
            }


            try
            {
                int bookComments = await bookService.GetBookCommentsCountAsync(id);
                Pager commentPager = new Pager(bookComments, page, CommentsPageSize);

                if (!await bookService.BookExistsAsync(id))
                {
                    return NotFound();
                }
                BookInfoViewModel bookInfo = await bookService.GetBookByIdAsync(id, commentPager, userId);
                bookInfo.CommentsPager = commentPager;

                return View(bookInfo);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Books));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Info(BookInfoViewModel bookInfoViewModel, int page)
        {
            Guid? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = User.GetId();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            var validationResult = bookInfoViewModel.Validate(new ValidationContext(bookInfoViewModel));
            if (!ModelState.IsValid || validationResult != null)
            {
                int bookComments = await bookService.GetBookCommentsCountAsync(bookInfoViewModel.Id);
                Pager commentPager = new Pager(bookComments, page, CommentsPageSize);
                var model = await bookService.GetBookByIdAsync(bookInfoViewModel.Id, commentPager, userId);
                model.CommentsPager = commentPager;
                model.UserQuantity = bookInfoViewModel.UserQuantity;

                ModelState.AddModelError("", validationResult.ErrorMessage);
                return View(model);
            }
            return RedirectToAction("OrderItem", "Order", bookInfoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BookPartial(string filePath, int id, int pageCount)
        {
            try
            {
                byte[] pdfContent = await bookService.GetBookFileAsync(filePath, BookPages);

                if (pdfContent != null && pdfContent.Length > 0)
                {
                    return new FileContentResult(pdfContent, "application/pdf");
                }

                return RedirectToAction(nameof(Info));
            }
            catch(Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Info));
            }
        }

        [HttpGet]
        public async Task<IActionResult> BooksByTitle(string title = "", int id = 0)
        {
            try
            {
                IEnumerable<BookNameViewModel> bookByTitle = await bookService.GetBookByTitleAsync(title, id);
                return PartialView("_BooksByTitle", bookByTitle);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Books));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            try
            {
                await bookService.AddItemToFavorites(id, User.GetId());
                TempData[SuccessMessage] = SuccessfullyAddedItemToFavorites;
                this.memoryCache.Remove(string.Format(UserFavoriteItemsCacheKey, this.User.GetId()));
                return Ok();
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Books));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            try
            {
                await bookService.RemoveItemFromFavorites(id, User.GetId());
                TempData[SuccessMessage] = SuccessfullyRemovedItemFromFavorites;
                this.memoryCache.Remove(string.Format(UserFavoriteItemsCacheKey, this.User.GetId()));
                return NoContent();
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Books));
            }
        }

        
    }
}
