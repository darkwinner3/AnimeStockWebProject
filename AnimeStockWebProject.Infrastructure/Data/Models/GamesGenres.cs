using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class GamesGenres
    {
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}