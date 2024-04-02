using AnimeStockWebProject.Areas.Admin.Models.User;

namespace AnimeStockWebProject.Areas.Admin.Models
{
    public class AdminPageViewModel
    {
        public AdminPageViewModel()
        {
            Users = new List<UsersViewModel>();
        }
        public IEnumerable<UsersViewModel> Users { get; set; }
    }
}
