using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
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
            ViewBag.ShowFooter = true;
            return View(allBookTags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                EditBookTagViewModel editBookTagViewModel = await bookTagService.GetBookTagToEditAsync(id);
                ViewBag.ShowFooter = true;
                return View(editBookTagViewModel);
            }
            catch (Exception)
            {
                ViewBag.ShowFooter = true;
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Dashboard", "Home", new { Area = AdminAreaName });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ShowFooter = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await bookTagService.DeleteBookTagByIdAsync(id);
                TempData[SuccessMessage] = SuccessfullyDeletedTag;
                this.memoryCache.Remove(BookTagsCacheDuration);
                this.memoryCache.Remove(BookTagAdminCacheKey);
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
                await bookTagService.RecoverBookTagByIdAsync(id);
                TempData[SuccessMessage] = SuccessfullyRecoveredTag;
                this.memoryCache.Remove(BookTagsCacheKey);
                this.memoryCache.Remove(BookTagAdminCacheKey);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookTagViewModel editBookTagViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editBookTagViewModel);
            }
            try
            {
                await bookTagService.EditBookTagByIdAsync(id, editBookTagViewModel);
                TempData[SuccessMessage] = SuccessfullyEditedTag;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Dashboard", "Home", new { Area = AdminAreaName });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditBookTagViewModel editBookTagViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editBookTagViewModel);
            }
            try
            {
                await bookTagService.CreateBookTagAsync(editBookTagViewModel);
                TempData[SuccessMessage] = SuccessfullyCreatedBookTag;
                this.memoryCache.Remove(BookTagsCacheKey);
                this.memoryCache.Remove(BookTagAdminCacheKey);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DefaultErrorMessage;
                return RedirectToAction("Dashboard", "Home", new { Area = AdminAreaName });
            }
        }
    }
}
