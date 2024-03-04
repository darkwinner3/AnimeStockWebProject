using AnimeStockWebProject.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserByIdAsync(Guid id);
        Task<UserInfoViewModel> GetUserInfoByIdAsync(Guid id);
        Task SaveUserInfoAsync(Guid id, UserInfoViewModel user);
    }
}
