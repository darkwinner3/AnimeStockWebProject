using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Core.Models.Pager;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class BookAdminService : IBookAdminService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookAdminService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }
        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync(Pager pager)
        {
            IEnumerable<BookViewModel> books = await animeStockDbContext.Books
                .OrderBy(b => b.IsDeleted)
                .Select(x => new BookViewModel()
            {
                Title = x.Title,
                PictureUrl = x.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                IsDeleted = x.IsDeleted,
                Id = x.Id,
            })
                .Skip((pager.CurrentPage - 1) * pager.PageSize)
                .Take(pager.PageSize)
                .ToArrayAsync();

            return books;
        }

        public async Task<int> GetAllBooksCountAsync()
        {
            var bookCount = await animeStockDbContext.Books.CountAsync();
            return bookCount;
        }
    }
}
