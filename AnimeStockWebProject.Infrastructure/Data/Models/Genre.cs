using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.GenreEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Genre
    {
        public Genre()
        {
            GameGenres = new List<GamesGenres>();
        }
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<GamesGenres> GameGenres { get; set; }
    }
}
