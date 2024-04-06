using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class PictureAdminService : IPictureAdminService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public PictureAdminService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }
       
        public async Task DeletePictureAsync(int pictureId)
        {
            var picture = await animeStockDbContext.Pictures.FirstAsync(p => p.Id == pictureId);

            picture.IsDeleted = true;
            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task<bool> PictureExistsByIdAsync(int pictureId)
        {
            return await animeStockDbContext.Pictures.AnyAsync(p => p.Id == pictureId);
        }

        public async Task<bool> PictureIsAlreadyDeletedAsync(int pictureId)
        {
            return await animeStockDbContext.Pictures.AnyAsync(p => p.Id == pictureId && p.IsDeleted);
        }

        public async Task<bool> PictureIsRecoveredAsync(int pictureId)
        {
            return await animeStockDbContext.Pictures.AnyAsync(p => p.Id == pictureId && !p.IsDeleted);
        }

        public async Task RecoverPictureAsync(int pictureId)
        {
            var picture = await animeStockDbContext.Pictures.FirstAsync(p => p.Id == pictureId);

            picture.IsDeleted = false;
            await animeStockDbContext.SaveChangesAsync();
        }
    }
}
