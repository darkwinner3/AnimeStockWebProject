using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class BookTagService : IBookTagService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookTagService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }
        public async Task<IEnumerable<TagViewModel>> GetBookTagsAsync()
        {
            IEnumerable<TagViewModel> bookTags = await animeStockDbContext.Tags
                .Where(t => !t.IsDeleted)
                .Select(x => new TagViewModel()
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                Name = x.Name
            }).ToArrayAsync();

            return bookTags;
        }

        public Task<EditBookTagViewModel> GetBookTagToEditAsync()
        {
            throw new NotImplementedException();
        }
    }
}
