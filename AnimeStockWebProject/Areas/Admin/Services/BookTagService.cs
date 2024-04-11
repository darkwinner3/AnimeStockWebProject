using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.BookTag;
using AnimeStockWebProject.Core.Models.BookTags;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class BookTagService : IBookTagService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public BookTagService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }

        public async Task DeleteBookTagByIdAsync(int id)
        {
            var tagToDelete = await animeStockDbContext.Tags.FirstAsync(t => t.Id == id);
            
            tagToDelete.IsDeleted = true;

            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task RecoverBookTagByIdAsync(int id)
        {
            var tagToRecover = await animeStockDbContext.Tags.FirstAsync(t => t.Id == id);

            tagToRecover.IsDeleted = false;

            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagViewModel>> GetBookTagsAsync()
        {
            IEnumerable<TagViewModel> bookTags = await animeStockDbContext.Tags
                .Select(x => new TagViewModel()
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                Name = x.Name
            }).ToArrayAsync();

            return bookTags;
        }

        public async Task<EditBookTagViewModel> GetBookTagToEditAsync(int id)
        {
            Tag tag = await animeStockDbContext.Tags.FirstAsync(bt => bt.Id == id);
            return new EditBookTagViewModel()
            {
                Name = tag.Name,
            };
        }

        public async Task EditBookTagByIdAsync(int id, EditBookTagViewModel editBookTagViewModel)
        {
            var tagToEdit = await animeStockDbContext.Tags.FirstAsync(bt => bt.Id == id);
            tagToEdit.Name = WebUtility.HtmlEncode(editBookTagViewModel.Name);
            
            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task CreateBookTagAsync(EditBookTagViewModel editBookTagViewModel)
        {
            Tag tag = new Tag()
            {
                Name = WebUtility.HtmlEncode(editBookTagViewModel.Name)
            };
            await animeStockDbContext.Tags.AddAsync(tag);
            await animeStockDbContext.SaveChangesAsync();
        }
    }
}
