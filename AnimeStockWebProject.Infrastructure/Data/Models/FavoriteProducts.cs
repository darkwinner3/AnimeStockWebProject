using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class FavoriteProducts
    {
        [Key]
        public int Id { get; set; }

        public int? BookId { get; set; }
        public Book? Book { get; set; }
        public int? GameId { get; set; }
        public Game? Game { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}