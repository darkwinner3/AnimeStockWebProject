using AnimeStockWebProject.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.GameEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Game
    {
        public Game()
        {
            GameGenres = new List<GamesGenres>();
            Comments = new List<Comment>();
            FavoriteProducts = new List<FavoriteProducts>();
            Orders = new List<Order>();
            Pictures = new List<Picture>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(DeveloperMaxLength)]
        public string Developer { get; set; } = string.Empty;

        [Required]
        [MaxLength(PublisherMaxLength)]
        public string Publisher { get; set; } = string.Empty;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public PrintTypeEnum PrintType { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public PlatformTypeEnum platformType { get; set; }

        public List<GamesGenres> GameGenres { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<FavoriteProducts> FavoriteProducts { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
