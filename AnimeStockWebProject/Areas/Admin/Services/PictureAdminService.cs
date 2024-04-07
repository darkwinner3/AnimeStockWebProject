using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.Book;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class PictureAdminService : IPictureAdminService
    {
        private readonly IWebHostEnvironment env;
        private readonly AnimeStockDbContext animeStockDbContext;

        public PictureAdminService(AnimeStockDbContext animeStockDbContext, IWebHostEnvironment env)
        {
            this.animeStockDbContext = animeStockDbContext;
            this.env = env;
        }

        public async Task DeletePictureAsync(int pictureId)
        {
            var picture = await animeStockDbContext.Pictures.FirstAsync(p => p.Id == pictureId);

            picture.IsDeleted = true;
            await animeStockDbContext.SaveChangesAsync();
        }
        //Deleting picture every 3 days
        public async Task DeletePicturesAsync()
        {
            var pictures = await animeStockDbContext.Pictures.ToArrayAsync();

            foreach (var picture in pictures)
            {
                if (picture.IsDeleted)
                {
                    string pictureFolderName = picture.Path;
                    string deletePath = Path.Join(env.WebRootPath, pictureFolderName);

                    File.Delete(deletePath);

                    animeStockDbContext.Remove(picture);
                }
            }

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
