using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Areas.Admin.Models.User
{
    public class UsersViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; } = null!;

        public DateTime Joined { get; set; }
    }
}
