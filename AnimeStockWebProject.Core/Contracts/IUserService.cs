﻿using AnimeStockWebProject.Core.Models.Book;
using AnimeStockWebProject.Core.Models.Order;
using AnimeStockWebProject.Core.Models.User;

namespace AnimeStockWebProject.Core.Contracts
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserByIdAsync(Guid id);
        Task<UserInfoViewModel> GetUserInfoByIdAsync(Guid id);
        Task SaveUserInfoAsync(Guid id, UserInfoViewModel user);
        Task DeleteUserProfilePictureAsync(Guid id, string path);
        Task<string> UploadUserImageAsync(UserInfoViewModel userInfo, Guid userId);
        Task<IEnumerable<BookViewModel>> GetUserFavoriteBooksAsync(Guid userId);
        Task<IEnumerable<UserOrderViewModel>> GetUserOrdersAsync(Guid userId);
    }
}
