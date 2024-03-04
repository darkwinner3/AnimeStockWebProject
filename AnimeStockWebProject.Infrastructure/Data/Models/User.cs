using Microsoft.AspNetCore.Identity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            FavoriteProducts = new List<FavoriteProducts>();
            Orders = new List<Order>();
            Comments = new List<Comment>();
            this.Id = Guid.NewGuid();
            JoinTime = DateTime.Now;
        }

        public string? FirstName { get; set; }

        public string? ProfilePicturePath { get; set; }

        public DateTime JoinTime { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FavoriteProducts> FavoriteProducts { get; set; }
    }
}
