using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class FavoriteProducts
    {
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        [ForeignKey(nameof(User))]
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}