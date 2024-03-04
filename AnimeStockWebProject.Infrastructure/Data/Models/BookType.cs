using System.ComponentModel.DataAnnotations;
using static AnimeStockWebProject.Common.EntityValidations.BookTypeEntity;

namespace AnimeStockWebProject.Infrastructure.Data.Models
{
    public class BookType
    {
        public BookType()
        {
            Books = new List<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; }

        public bool IsDeleted { get; set; }

    }
}
