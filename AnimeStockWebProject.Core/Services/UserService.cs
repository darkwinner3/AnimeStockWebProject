using AnimeStockWebProject.Core.Contracts;
using AnimeStockWebProject.Core.Models.User;
using AnimeStockWebProject.Infrastructure.Data;
using AnimeStockWebProject.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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
                    UserId = x.Id,
                    FirstName = x.FirstName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                }).FirstAsync(u => u.UserId == id);
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

        
    }
}
