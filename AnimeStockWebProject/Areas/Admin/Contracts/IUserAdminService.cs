using AnimeStockWebProject.Areas.Admin.Models.User;

namespace AnimeStockWebProject.Areas.Admin.Contracts
{
    public interface IUserAdminService
    {
        Task<IEnumerable<UsersViewModel>> GetAllUsersAsync();
    }
}
