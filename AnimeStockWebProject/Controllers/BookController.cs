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

namespace AnimeStockWebProject.Controllers
{
    using static Common.GeneralAplicaitonConstants;
    public class BookController : Controller
    {
        private readonly ITagService tagService;
        private readonly IBookService bookService;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeService typeService;
        public BookController(ITagService tagService, IBookService bookService,
            IMemoryCache memoryCache, ITypeService typeService)
        {
            this.tagService = tagService;
            this.bookService = bookService;
            this.memoryCache = memoryCache;
            this.typeService = typeService;
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

            Pager pager = new Pager(await bookService.GetCountAsync(bookQueryViewModel), bookQueryViewModel.currentPage);
            bookQueryViewModel.Pager = pager;
            AllBooksSortedDataModel sortedBooks = await bookService.GetAllBooksSortedDataModelAsync(userId, bookQueryViewModel);
            bookQueryViewModel.BookViewModels = sortedBooks.Books;

            return View(bookQueryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }
            try
            {
                int bookComments = await bookService.GetBookCommentsCountAsync(id);
                Pager commentPager = new Pager(bookComments, page);

                if (!await bookService.BookExistsAsync(id))
                {
                    return NotFound();
                }
                BookInfoViewModel bookInfo = await bookService.GetBookByIdAsync(id, commentPager);
                bookInfo.CommentsPager = commentPager;
                return View(bookInfo);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction(nameof(Books));
            }
        }
    }
}
