using AnimeStockWebProject.Areas.Admin.Contracts;
using AnimeStockWebProject.Areas.Admin.Models.User;
using AnimeStockWebProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeStockWebProject.Areas.Admin.Services
{
    public class UserAdminService : IUserAdminService
    {
        private readonly AnimeStockDbContext animeStockDbContext;

        public UserAdminService(AnimeStockDbContext animeStockDbContext)
        {
            this.animeStockDbContext = animeStockDbContext;
        }
        public async Task<IEnumerable<UsersViewModel>> GetAllUsersAsync()
        {
            var users = await this.animeStockDbContext.Users.Select(u => new UsersViewModel()
            {
                Email = u.Email,
                FirstName = u.FirstName,
                Id = u.Id,
                Joined = u.JoinTime,
                UserName = u.UserName
            })
                .ToArrayAsync();
            return users;
        }
    }
}
