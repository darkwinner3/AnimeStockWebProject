using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static AnimeStockWebProject.Common.GeneralAplicaitonConstants;
using static AnimeStockWebProject.Common.NotifiactionMessages;
using static AnimeStockWebProject.Common.NotificationKeys;

namespace AnimeStockWebProject.Areas.Admin.Controllers
{
    public class BookTagController : AdminController
    {
        private readonly IBookTagService bookTagService;
        private readonly IMemoryCache memoryCache;

        public BookTagController(IBookTagService bookTagService, IMemoryCache memoryCache)
        {
            this.bookTagService = bookTagService;
            this.memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<TagViewModel> allBookTags = this.memoryCache.Get<IEnumerable<TagViewModel>>(BookTagAdminCacheKey);
            if (allBookTags == null)
            {
                allBookTags = await  bookTagService.GetBookTagsAsync();
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(BookTagAdminCacheDuration));
                this.memoryCache.Set(BookTagAdminCacheKey, allBookTags, options);
            }

            return View(allBookTags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                return View();
                //EditBookTagViewModel editBookTagViewModel = await bookTagService
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
