using AnimeStockWebProject.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AnimeStockWebProject.Common.EntityValidations.BookEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class Book
    {
        public Book()
        {
            BookTags = new List<BooksTags>();
            Comments = new List<Comment>();
            FavoriteProducts = new List<FavoriteProducts>();
            Orders = new List<Order>();
            Pictures = new List<Picture>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(IllustratorMaxLength)]
        public string Illustrator { get; set; } = string.Empty;

        [Required]
        [MaxLength(PublisherMaxLength)]
        public string Publisher { get; set;} = string.Empty;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(BookType))]
        public int BookTypeId { get; set; }

        [Required]
        public BookType BookType { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(MaxPages)]
        public int Pages {  get; set; }

        public int Quantity { get; set; }

        [Required]
        public PrintTypeEnum PrintType { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsFavorite { get; set;}


        public List<BooksTags> BookTags { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<FavoriteProducts> FavoriteProducts { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
