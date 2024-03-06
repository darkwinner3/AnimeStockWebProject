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
    }
}
