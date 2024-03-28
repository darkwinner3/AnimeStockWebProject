using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Core.Models.User;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AnimeStockWebProject.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AnimeStockDbContext animeStockDbContext;
        private readonly IWebHostEnvironment env;

        public UserService(AnimeStockDbContext animeStockDbContext, IWebHostEnvironment env)
        {
            this.animeStockDbContext = animeStockDbContext;
            this.env = env;
        }

        public async Task<UserViewModel> GetUserByIdAsync(Guid id)
        {
            UserViewModel userViewModel = await animeStockDbContext.Users
                .Select (x => new UserViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                }).FirstAsync(u => u.Id == id);
            return userViewModel;
        }

        public async Task<UserInfoViewModel> GetUserInfoByIdAsync(Guid userId)
        {
            UserInfoViewModel userInfo = await animeStockDbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserInfoViewModel()
                {
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    FirstName = u.FirstName ?? "",
                    ProfilePicturePath = u.ProfilePicturePath
                }).FirstAsync();
            return userInfo;
        }

        public async Task SaveUserInfoAsync(Guid id, UserInfoViewModel userInfo)
        {
            User userToFind = await animeStockDbContext.Users
                .FirstAsync(u => u.Id == id);
            userToFind.FirstName = userInfo.FirstName;
            userToFind.PhoneNumber = userInfo.PhoneNumber;
            userToFind.Email = userInfo.Email;
            userToFind.ProfilePicturePath = userInfo.ProfilePicturePath;
            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserProfilePictureAsync(Guid userId, string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string profilePictureName = path.Split("\\")[2];

                //change path when using the app
                string profilePictureFolderPath = Path.GetFullPath(@"D:\Important Learning\Programming\web projects\AnimeStockWebProject\AnimeStockWebProject\wwwroot\img\ProfilePictures\");
                string[] files = Directory.GetFiles(profilePictureFolderPath);
                
                if(files.Length > 0)
                {
                    string fileToDelete = files.FirstOrDefault(f => f.EndsWith(profilePictureName));
                    if (fileToDelete != null)
                    {
                        File.Delete(fileToDelete);
                    }
                }
            }

            User user = await animeStockDbContext.Users
                .FirstAsync(u => u.Id == userId);

            user.ProfilePicturePath = null;

            await animeStockDbContext.SaveChangesAsync();
        }

        public async Task<string> UploadUserImageAsync(UserInfoViewModel userInfo, Guid userId)
        {
            string fileName = userId.ToString() + "_" + userInfo.ProfilePictureFile.FileName;

            string newFilePath = Path.Combine("img", "ProfilePictures", fileName);

            using (FileStream stream = new FileStream(Path.Combine(env.WebRootPath, newFilePath), FileMode.Create))
            {
                await userInfo.ProfilePictureFile.CopyToAsync(stream);
            }
            return newFilePath;
        }

        public async Task<IEnumerable<BookViewModel>> GetUserFavoriteBooksAsync(Guid userId)
        {
            IEnumerable<BookViewModel> userBooks = await animeStockDbContext
                .FavoriteProducts
                .Where(p => p.UserId == userId && !p.Book.IsDeleted)
                .Select(fp => new BookViewModel()
                {
                    Id = fp.Book.Id,
                    Title = fp.Book.Title,
                    Author = fp.Book.Author,
                    Illustrator = fp.Book.Illustrator,
                    Description = fp.Book.Description,
                    BookType = fp.Book.BookType.Name,
                    ReleaseDate = fp.Book.ReleaseDate.Date,
                    PrintType = fp.Book.PrintType.ToString(),
                    Price = fp.Book.Price,
                    PicturePath = fp.Book.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                    IsFavorite = true
                })
                .ToArrayAsync();
            return userBooks;
        }

        public async Task<IEnumerable<UserOrderViewModel>> GetUserOrdersAsync(Guid userId)
        {
            return await animeStockDbContext.Orders.Where(o => o.UserId == userId)
                .Select(o => new UserOrderViewModel()
                {
                    Id = o.Id,
                    UserId = userId,
                    OrderDate = o.OrderDate,
                    Picture = o.Book.Pictures.FirstOrDefault(p => !p.IsDeleted && p.Path.Contains("cover")).Path,
                    Price = o.TotalPrice,
                    Title = o.Book.Title,
                    Status = o.Status.ToString(),
                    UserQuantity = o.UserOrders,
                    printType = o.Book.PrintType.ToString(),
                    BookId = o.Book.Id
                })
                .ToArrayAsync();
        }
    }
}
