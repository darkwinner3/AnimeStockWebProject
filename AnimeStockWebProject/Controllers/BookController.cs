using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Core.Models.BookType;
using AnimeStockWebProject.Core.Models.Pager;
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
        public async Task<IActionResult> Books([FromQuery] BookQueryViewModel bookQueryViewModel)
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
            Pager pager = new Pager(await bookService.GetCountAsync(bookQueryViewModel), bookQueryViewModel.currentPage);
            bookQueryViewModel.Pager = pager;
            AllBooksSortedDataModel sortedBooks = await bookService.GetAllBooksSortedDataModelAsync(userId, bookQueryViewModel);
            bookQueryViewModel.BookViewModels = sortedBooks.Books;
            
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

            return View(bookQueryViewModel);
        }
    }
}
