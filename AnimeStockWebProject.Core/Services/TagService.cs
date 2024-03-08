using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Core.Services
{
    public class TagService : ITagService
    {
        private readonly AnimeStockDbContext animeStockDb;

        public TagService(AnimeStockDbContext animeStockDb)
        {
            this.animeStockDb = animeStockDb;
        }
        public async Task<IEnumerable<TagViewModel>> GetAllTagsAsync()
        {
            IEnumerable<TagViewModel> tags = await animeStockDb
                .Tags.Where(t => !t.IsDeleted)
                .Select(t => new TagViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .ToArrayAsync();
            return tags;
        }
    }
}
