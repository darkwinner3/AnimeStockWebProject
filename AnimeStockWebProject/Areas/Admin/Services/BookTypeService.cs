using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookType;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class BookTypeService : IBookTypeService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookTypeService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }
        public async Task<IEnumerable<BookTypeViewModel>> GetAllBookTypesAsync()
        {
            IEnumerable<BookTypeViewModel> bookTypes = await animeStockDbContext.BookTypes
                .Select(bt => new BookTypeViewModel()
                {
                    Id = bt.Id,
                    Name = bt.Name,
                })
                .ToArrayAsync();
            return bookTypes;
        }
    }
}
